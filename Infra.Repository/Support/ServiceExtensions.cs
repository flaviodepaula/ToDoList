using Domain.Users.Interfaces;
using Infra.Repository.Context;
using Infra.Repository.User.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Repository.Support
{
    public static class ServiceExtensions
    {
        public static void AddRepositoryExtentions(this IServiceCollection services, IConfiguration configuration)
        {
            var strConn = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(strConn, ServerVersion.AutoDetect(strConn))
            );

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
