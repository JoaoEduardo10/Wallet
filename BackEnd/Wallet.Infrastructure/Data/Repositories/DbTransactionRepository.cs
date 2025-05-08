using Microsoft.EntityFrameworkCore.Storage;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Infrastructure.Data.Repositories
{
    public class DbTransactionRepository(WalletDbContext _walletDbContext)
    {

        private int TransactionRunning = 0;

        private IDbContextTransaction WalletTransaction;

        public async Task BeginTransactionAsync()
        {
            if (TransactionRunning > 0)
            {
                TransactionRunning++;
                return;
            }

            TransactionRunning++;

            WalletTransaction = await _walletDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            TransactionRunning--;

            if (TransactionRunning == 0)
            {
                await WalletTransaction.CommitAsync();

                WalletTransaction.Dispose();

                WalletTransaction = null;
            }
        }

        public async Task RollBackTransactionAsync()
        {
            TransactionRunning--;

            if (TransactionRunning == 0)
            {
                await WalletTransaction.RollbackAsync();

                WalletTransaction.Dispose();

                WalletTransaction = null;

                try
                {
                    var walletEnties = _walletDbContext.ChangeTracker.Entries().ToList();

                    foreach (var entity in walletEnties)
                    {
                        if (entity.Entity != null)
                        {
                            entity.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    throw;
                }
            }
        }
    }
}
