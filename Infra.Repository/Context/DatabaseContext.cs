using Infra.Repository.Tasks.Entity;
using Infra.Repository.Tasks.Mappers;
using Infra.Repository.User.Entities;
using Infra.Repository.User.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new TaskMapping());
        }
    }
}
