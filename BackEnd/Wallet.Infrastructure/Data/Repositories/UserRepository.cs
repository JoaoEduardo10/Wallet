using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WalletDbContext db) : base(db) { }

        public IQueryable<User> GetAllWithCollections()
        {
            return GetAll()
                .Include(u => u.Wallet);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await Db.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
