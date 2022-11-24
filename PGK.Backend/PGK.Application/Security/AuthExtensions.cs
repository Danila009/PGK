using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PGK.Application.Security
{
    internal static class AuthExtensions
    {
        public static IServiceCollection AddAuthSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["token:audience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["token:issuer"],
                        ValidateLifetime = true,
                        IssuerSigningKey = Auth.GetSymmetricSecurityKey(configuration["token:key"]),
                        ValidateIssuerSigningKey = true
                    };
                });

            return services;
        }
    }
}
