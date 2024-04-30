using Domain.Tasks.Interfaces;
using Domain.Users.Interfaces;
using Infra.Repository.Context;
using Infra.Repository.Tasks.Service;
using Infra.Repository.User.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Repository.Support
{
    public static class ServiceExtensions
    {
        public static void AddRepositoryExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            var strConn = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(strConn, ServerVersion.AutoDetect(strConn))
            );

            services.AddScoped<ITaskRepository, TaskRepositoryService>();
            services.AddScoped<IUserRepository, UserRepositoryService>();
        }
    }
}
