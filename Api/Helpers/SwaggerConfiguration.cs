using Microsoft.OpenApi.Models;

namespace Api.Helpers;
public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerApplication(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projects-admin Swagger", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please provide JWT with bearer (Bearer {jwt token})",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    new List<string>() }
            });
        });
        return services;
    }
}
