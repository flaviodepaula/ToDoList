using Infra.Repository.Tasks.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.Tasks.Mappers
{
    public class TaskMapping : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(tarefa => tarefa.IdTask);
            builder.Property(tarefa => tarefa.Description).HasMaxLength(100).HasColumnType("varchar").IsRequired(true);
            builder.Property(tarefa => tarefa.Title).HasMaxLength(50).HasColumnType("varchar").IsRequired(true);
            builder.Property(tarefa => tarefa.CreationDate).IsRequired(true);
            
            builder.Property(tarefa => tarefa.UserEmail).IsRequired(true);
            

            builder.HasOne(x => x.User)
                .WithMany(y => y.Tasks)
                .HasForeignKey(x => x.UserEmail);
        }
    }
}
