using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Wallet.Application.Interfaces;
using Wallet.Application.Business;
using Wallet.Domain.Interfaces;

namespace Wallet.Api.Configuration.Token
{
    public static class IdentityConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var jwtKey = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
                    };
                });

            services.AddSingleton<IAuthentication>(serviceProvider =>
            {
                var userRepository = serviceProvider.GetRequiredService<IUserRepository>();

                return new AuthenticationBusiness(jwtSettings.Secret, jwtSettings.Lifespan, userRepository);
            });
        }
    }
}
