using Wallet.Application.Dtos.User;
using Wallet.Application.Interfaces;
using Wallet.Application.Mapping;
using Wallet.Application.Models;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Utilities;
using Wallet.Infrastructure.Data.Repositories;

namespace Wallet.Application.Business
{
    public class UserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IAuthentication _authentication;

        private readonly DbTransactionRepository _dbTransactionRepository;

        public UserBusiness(IUserRepository userRepository, DbTransactionRepository dbTransactionRepository, IWalletRepository walletRepository, IAuthentication authentication)
        {
            _userRepository = userRepository;
            _dbTransactionRepository = dbTransactionRepository;
            _walletRepository = walletRepository;
            _authentication = authentication;
        }

        public async Task<Result<Authentication>> LoginUserAsync(LoginUserDto login)
        {
            var user = await _userRepository.GetUserByEmailAsync(login.Email);

            if (user is null)
            {
                return new Result<Authentication>("E-mail ou senha estão incorretos.");
            }

            var isPasswordCorrect = user.VerifyPassword(login.Password);

            if (!isPasswordCorrect)
            {
                return new Result<Authentication>("E-mail ou senha estão incorretos.");
            }

            var resultAuthentication =  _authentication.GetAuthentication(user.Id, user.Name);

            if (!resultAuthentication.Success)
            {
                return new Result<Authentication>(resultAuthentication.Errors);
            }

            return Result.ToValue(resultAuthentication.Value);
        }

        public async Task<Result<CreateUserDto>> CreateUserAsync(CreateUserDto user)
        {
            try
            {
                var existsUser = await _userRepository.GetUserByEmailAsync(user.Email);

                if (existsUser is not null)
                {
                    return new Result<CreateUserDto>("Email já cadastrado");
                }

                var newUser = UserMapping.ToEntity(user);

                var result = newUser.ValidateUser();

                if (!result.Success)
                {
                    return new Result<CreateUserDto>(result.Errors);
                }

                newUser.Wallet.CreateWallet();

                newUser.HashPassword();

                await _dbTransactionRepository.BeginTransactionAsync();

                _userRepository.Add(newUser);

                _walletRepository.Add(newUser.Wallet);

                await _userRepository.SaveChangesAsync();

                await _dbTransactionRepository.CommitTransactionAsync();

                return Result<CreateUserDto>.ToValue(UserMapping.ToCreateUserDto(newUser));
            }
            catch
            {
                await _dbTransactionRepository.RollBackTransactionAsync();

                throw;
            }
        }
    }
}
