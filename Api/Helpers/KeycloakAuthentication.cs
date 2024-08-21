using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers;
public static class KeycloakAuthentication
{
    public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.MetadataAddress = $"{configuration["Keycloak:identity"]}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
