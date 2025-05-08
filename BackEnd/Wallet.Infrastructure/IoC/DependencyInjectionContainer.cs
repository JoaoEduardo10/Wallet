using Microsoft.Extensions.DependencyInjection;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Data.Context;
using Wallet.Infrastructure.Data.Repositories;

namespace Wallet.Infrastructure.IoC
{
    public static class DependencyInjectionContainer
    {
        public static void RegisterInfrastructureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<DbTransactionRepository>();

            services.AddScoped<WalletDbContext>();
            
        }
    }
}
