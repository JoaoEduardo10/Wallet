using Wallet.Domain.Entities;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Api
{
    public class SeedData(WalletDbContext context)
    {
       public void Add()
        {
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Name = "Joaquim Silva", Email = "joaquim@wallet.com" },
                    new User { Name = "Francisco Lima", Email = "francisco@wallet.com" },
                    new User { Name = "Ana Beatriz", Email = "ana.beatriz@wallet.com" },
                    new User { Name = "Carlos Souza", Email = "carlos.souza@wallet.com" },
                    new User { Name = "Mariana Rocha", Email = "mariana.rocha@wallet.com" },
                    new User { Name = "Pedro Henrique", Email = "pedro.henrique@wallet.com" },
                    new User { Name = "Camila Duarte", Email = "camila.duarte@wallet.com" },
                    new User { Name = "Lucas Mendes", Email = "lucas.mendes@wallet.com" },
                    new User { Name = "Fernanda Costa", Email = "fernanda.costa@wallet.com" },
                    new User { Name = "Rafael Oliveira", Email = "rafael.oliveira@wallet.com" }
                };

                foreach (var user in users)
                {
                    user.Password = "12345678";
                    user.CreatedAt = DateTime.UtcNow;
                    user.Wallet = new Domain.Entities.Wallet();

                    user.Wallet.CreateWallet();
                    user.HashPassword();
                }

                context.Users.AddRange(users);
                context.Wallets.AddRange(users.Select(u => u.Wallet));
                context.SaveChanges();

            }
        }
    }
}
