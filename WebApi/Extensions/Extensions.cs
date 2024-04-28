using Domain.Users.Models;
using WebApi.DTOs;

namespace WebApi.Extensions
{
    public static class Extensions
    {
        public static User ToUserModel(this UserDto userDto)
        {
            return new User()
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role,
                UserName = userDto.UserName,    
            };
        }

        public static UserDto ToUserDtoModel(this User userDto)
        {
            return new UserDto()
            {
                Email = userDto.Email,
                Role = userDto.Role,
                UserName = userDto.UserName,
            };
        }
    }
}
