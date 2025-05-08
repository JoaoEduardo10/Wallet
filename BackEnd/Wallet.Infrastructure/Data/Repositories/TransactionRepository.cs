using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Infrastructure.Data.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(WalletDbContext db) : base(db) { }

        public IQueryable<Transaction> GetAllWithCollections()
        {
            return GetAll()
                .Include(t => t.SenderWallet)
                .Include(t => t.ReceiverWallet);
        }
    }
}
