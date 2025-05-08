using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Infrastructure.Data.Repositories
{
    public class WalletRepository : GenericRepository<Wallet.Domain.Entities.Wallet>, IWalletRepository
    {
        public WalletRepository(WalletDbContext db) : base(db) { }

        public Task<Domain.Entities.Wallet?> GetWalletByEmailAsync(string email)
        {
            return Db.Wallets
                .Include(w => w.User)
                .Where(w => w.User.Email == email)
                .SingleOrDefaultAsync();
        }

        public async Task<Domain.Entities.Wallet?> GetWalletByIdAsync(Guid id)
        {        
            return await Db.Wallets
                .Include(w => w.User)
                .SingleOrDefaultAsync(w => w.Id == id);
        }

        public Task<Domain.Entities.Wallet?> GetWalletByUserIdAsync(Guid userId)
        {
            return Db.Wallets.SingleOrDefaultAsync(w => w.UserId == userId);
        }
    }
}
