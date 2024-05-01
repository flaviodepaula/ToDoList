using Microsoft.OpenApi.Models;

namespace WebApi.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "ToDo List Api",
                    Description = "Api de exemplo de gerenciameno de listas de atividades",
                    Contact = new OpenApiContact()
                    {
                        Email = "flaviodepaula@gmail.com",
                        Name = "Flávio de Paula",
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
