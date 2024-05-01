using Infra.Repository.User.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.User.Mappers
{
    public class UserMapping : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(user => user.Email);
            
            builder.Property(user => user.UserName).HasMaxLength(30).HasColumnType("varchar").IsRequired(true);
            builder.Property(user => user.Password).HasMaxLength(256).HasColumnType("varchar").IsRequired(true);
            builder.Property(user => user.Email).HasMaxLength(50).HasColumnType("varchar").IsRequired(true);
            builder.Property(user => user.Role).HasMaxLength(30).IsRequired(true);
        }
    }
}
