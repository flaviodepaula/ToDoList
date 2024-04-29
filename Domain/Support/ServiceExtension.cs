using Domain.Authentication.Interfaces;
using Domain.Authentication.Services;
using Domain.Crypto;
using Domain.Tasks.Interfaces;
using Domain.Tasks.Service;
using Domain.Users.Interfaces;
using Domain.Users.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Support
{
    public static class ServiceExtension
    {
        public static void AddDomainExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITaskDomain, TaskDomainService>();
            services.AddScoped<IUserDomain, UserDomainService>();
        }
    }
}
