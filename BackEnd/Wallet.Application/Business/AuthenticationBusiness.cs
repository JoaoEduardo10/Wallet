using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Wallet.Application.Interfaces;
using Wallet.Application.Models;
using Wallet.Domain.Utilities;

namespace Wallet.Application.Business
{
    public class AuthenticationBusiness : IAuthentication
    {
        private string TokenSecret { get; }
        private int TokenLifespan { get; }
        public AuthenticationBusiness(string tokenSecret, int tokenLifespan)
        {
            TokenSecret = tokenSecret;
            TokenLifespan = tokenLifespan;
        }

        public Result<Authentication> GetAuthentication(Guid userId, string name)
        {
            var tokenExpirationTime = DateTime.Now.AddSeconds(TokenLifespan);

            var notBeforeTime = DateTime.Now;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, name),
                }),
                Expires = tokenExpirationTime,
                NotBefore = notBeforeTime,
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
                Username = name,
                IsAuthenticated = true,
                TokenExpirationTime = ((DateTimeOffset)tokenExpirationTime).ToUnixTimeSeconds(),
            };

            return Result.ToValue(authentication);
        }
    }
}
