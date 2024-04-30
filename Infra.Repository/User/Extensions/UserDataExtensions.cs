using Infra.Repository.User.Entities;

namespace Infra.Repository.User.Extensions
{
    public static class UserDataExtensions
    {
        public static Domain.Users.Models.User ToDomainModel(this UserEntity userEntity)
        {
            return new Domain.Users.Models.User()
            {
                Email = userEntity.Email,                
                Password = userEntity.Password,
                Role = userEntity.Role,
                UserName = userEntity.UserName
            };
        }
    }
}
