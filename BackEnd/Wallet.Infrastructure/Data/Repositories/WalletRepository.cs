using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Infrastructure.Data.Repositories
{
    public class WalletRepository : GenericRepository<Wallet.Domain.Entities.Wallet>, IWalletRepository
    {
        public WalletRepository(WalletDbContext db) : base(db) { }

        public IQueryable<Domain.Entities.Wallet> GetAllWithCollections()
        {
            return GetAll()
                .Include(w => w.User)
                .Include(w => w.Transactions);
        }
    }
}
