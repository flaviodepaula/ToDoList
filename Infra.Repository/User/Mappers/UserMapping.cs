using Infra.Repository.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.User.Mappers
{
    public class UserMapping : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(tarefa => tarefa.Id);
            builder.Property(tarefa => tarefa.UserName).HasMaxLength(30).HasColumnType("varchar").IsRequired(true);
            builder.Property(tarefa => tarefa.Password).HasMaxLength(256).HasColumnType("varchar").IsRequired(true);
            builder.Property(tarefa => tarefa.Email).HasMaxLength(50).HasColumnType("varchar").IsRequired(true);
            builder.Property(tarefa => tarefa.Role).HasMaxLength(30).IsRequired(true);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
