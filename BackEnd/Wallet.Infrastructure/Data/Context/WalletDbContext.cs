using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Infrastructure.Data.Mapping;

namespace Wallet.Infrastructure.Data.Context
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new WalletMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet.Domain.Entities.Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
