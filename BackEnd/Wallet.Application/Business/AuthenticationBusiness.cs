using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Wallet.Application.Interfaces;
using Wallet.Application.Models;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Utilities;

namespace Wallet.Application.Business
{
    public class AuthenticationBusiness : IAuthentication
    {
        private string TokenSecret { get; }
        private int TokenLifespan { get; }

        private readonly IUserRepository _userRepository;

        public AuthenticationBusiness(string tokenSecret, int tokenLifespan, IUserRepository userRepository)
        {
            TokenSecret = tokenSecret;
            TokenLifespan = tokenLifespan;
            _userRepository = userRepository;
        }

        public async Task<Result<Authentication>> GetAuthenticationAsync(Guid userId)
        {
            var tokenExpirationTime = DateTime.Now.AddSeconds(TokenLifespan);

            var user = await  _userRepository.GetByIdAsync(userId);

            if (user is null)
            {
                return new Result<Authentication>("Não foi possivel autenticar");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                }),
                Expires = tokenExpirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSecret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            var authentication = new Authentication()
            {
                Id = userId,
                Token = token,
                Username = user.Name,
                IsAuthenticated = true,
                TokenExpirationTime = ((DateTimeOffset)tokenExpirationTime).ToUnixTimeSeconds(),
            };

            return Result.ToValue(authentication);
        }
    }
}
