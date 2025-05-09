using Microsoft.EntityFrameworkCore;
using Wallet.Application.Dtos.Wallet;
using Wallet.Domain.Entities;
using Wallet.Domain.Entities.Enums;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Utilities;
using Wallet.Infrastructure.Data.Repositories;

namespace Wallet.Application.Business
{
    public class WalletBusiness
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        private readonly DbTransactionRepository _dbTransactionRepository;

        public WalletBusiness(IWalletRepository walletRepository, DbTransactionRepository dbTransactionRepository, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _dbTransactionRepository = dbTransactionRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<Domain.Entities.Wallet>> GetWalletByUserIdAsync(Guid userId)
        {
            var wallet = await _walletRepository.GetWalletByUserIdAsync(userId);

            if (wallet is null)
            {
                return new Result<Domain.Entities.Wallet>("Carteira não encontrada");
            }

            return Result.ToValue(wallet);
        }

        public async Task<Result<Domain.Entities.Wallet>> TransferAmountAsync(TransferAmountDto transfer) 
        { 
            try
            {
                var senderWallet = await _walletRepository.GetWalletByIdAsync(transfer.SenderWalletId);
                var receiverWallet = await _walletRepository.GetWalletByEmailAsync(transfer.ReceiverEmail);

                if (senderWallet is null)
                {
                    return new Result<Domain.Entities.Wallet>("carteira do remetente não encontrada");
                }

                if (receiverWallet is null)
                {
                    return new Result<Domain.Entities.Wallet>("carteira do destinatário não encontrado");
                }

                var resultSenderWallet = senderWallet.RemoveAmount(transfer.Amount);

                if (!resultSenderWallet.Success)
                {
                    return new Result<Domain.Entities.Wallet>(resultSenderWallet.Errors);
                }

                var resultReceiverWallet = receiverWallet.AddAmount(transfer.Amount);

                if (!resultReceiverWallet.Success)
                {
                    return new Result<Domain.Entities.Wallet>(resultReceiverWallet.Errors);
                }

                var transaction = new Transaction
                {
                    SenderWalletId = senderWallet.Id,
                    ReceiverWalletId = receiverWallet.Id,
                    Amout = transfer.Amount,
                    Status = TransactionStatus.Completed,
                    CreatedAt = new DateTime(2023, 1, 24, 0, 0, 0, DateTimeKind.Utc)
                };


                await _dbTransactionRepository.BeginTransactionAsync();  

                _walletRepository.Update(receiverWallet);
                _walletRepository.Update(senderWallet);
                _transactionRepository.Add(transaction);

                await _walletRepository.SaveChangesAsync();
                await _dbTransactionRepository.CommitTransactionAsync();

                return Result.ToValue(senderWallet);

            }
            catch
            {
                await _dbTransactionRepository.RollBackTransactionAsync();
                throw;
            }
        }


        public async Task<Result<Domain.Entities.Wallet>> AddBalanceAsync(Guid id, decimal amount)
        {
            try
            {
                var wallet = await _walletRepository.GetWalletByIdAsync(id);

                if (wallet is null)
                {
                    return new Result<Domain.Entities.Wallet>("Carteira não encontrada");
                }

                var resultWallet = wallet.AddAmount(amount);

                if (!resultWallet.Success)
                {
                    return new Result<Domain.Entities.Wallet>(resultWallet.Errors);
                }

                await _dbTransactionRepository.BeginTransactionAsync();

                _walletRepository.Update(wallet);

                await _walletRepository.SaveChangesAsync();

                await _dbTransactionRepository.CommitTransactionAsync();

                return Result.ToValue(wallet);
            }
            catch
            {

                await _dbTransactionRepository.RollBackTransactionAsync();

                throw;
            }
        }
    }
}
