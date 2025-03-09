using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic;

public static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authSettings = configuration.GetSection(nameof(AuthSettings)).Get<AuthSettings>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey))
            };
        });
        return services;
    }
}