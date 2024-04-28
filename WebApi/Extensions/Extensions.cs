using Domain.Users.Models;
using WebApi.DTOs;

namespace WebApi.Extensions
{
    public static class Extensions
    {
        public static User ToUserModel(this UserRequestDto userDto)
        {
            return new User()
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role,
                UserName = userDto.UserName,    
            };
        }

        public static UserResponseDto ToUserResponseDto(this User userDto)
        {
            return new UserResponseDto()
            {
                Email = userDto.Email,
                Role = userDto.Role,
                UserName = userDto.UserName,
            };
        }

        public static IEnumerable<UserResponseDto> ToUserResponseDto(this IEnumerable<User> userDto)
        {
            return userDto.Select(x => ToUserResponseDto(x));            
        }
    }
}
