using Microsoft.EntityFrameworkCore;
using Wallet.Infrastructure.Data.Context;

namespace Wallet.Api.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<WalletDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection")));
        }
    }
}
