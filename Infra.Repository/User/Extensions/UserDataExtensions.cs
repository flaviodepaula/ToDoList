using Infra.Repository.User.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.User.Extensions
{
    public static class UserDataExtensions
    {
        public static Domain.Users.Models.User ToDomainModel(this UserEntity userEntity)
        {
            return new Domain.Users.Models.User()
            {
                Email = userEntity.Email,
                Id = userEntity.Id,
                Password = userEntity.Password,
                Role = userEntity.Role,
                UserName = userEntity.UserName
            };
        }
    }
}
