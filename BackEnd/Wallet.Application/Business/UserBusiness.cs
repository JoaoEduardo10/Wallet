using Wallet.Application.Dtos.User;
using Wallet.Application.Mapping;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Utilities;
using Wallet.Infrastructure.Data.Repositories;

namespace Wallet.Application.Business
{
    public class UserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly DbTransactionRepository _dbTransactionRepository;

        public UserBusiness(IUserRepository userRepository, DbTransactionRepository dbTransactionRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _dbTransactionRepository = dbTransactionRepository;
            _walletRepository = walletRepository;
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

                return Result<CreateUserDto>.FromValue(UserMapping.ToDto(newUser));
            }
            catch
            {
                await _dbTransactionRepository.RollBackTransactionAsync();

                throw;
            }
        }
    }
}
