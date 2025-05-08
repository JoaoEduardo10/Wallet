using Wallet.Application.Business;
using Wallet.Infrastructure.IoC;

namespace Wallet.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentException(nameof(services));

            DependencyInjectionContainer.RegisterInfrastructureServices(services);

            services.AddScoped<UserBusiness>();
            services.AddScoped<WalletBusiness>();
        }
    }
}
