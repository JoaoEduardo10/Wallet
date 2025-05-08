using Wallet.Application.Business;
using Wallet.Infrastructure.IoC;

namespace Wallet.Api.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentException(nameof(services));

            DependencyInjectionContainer.RegisterInfrastructureServices(services);

            services.AddScoped<UserBusiness>();
        }
    }
}
