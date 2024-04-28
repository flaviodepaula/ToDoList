using Domain.Users.Models;
using WebApi.ViewModel;

namespace WebApi.Extensions
{
    public static class Extensions
    {
        public static User ToUserModel(this UserRequestViewModel userDto)
        {
            return new User()
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role,
                UserName = userDto.UserName,    
            };
        }

        public static UserResponseViewModel ToUserResponseDto(this User userDto)
        {
            return new UserResponseViewModel()
            {
                Email = userDto.Email,
                Role = userDto.Role,
                UserName = userDto.UserName,
            };
        }

        public static IEnumerable<UserResponseViewModel> ToUserResponseDto(this IEnumerable<User> userDto)
        {
            return userDto.Select(x => ToUserResponseDto(x));            
        }
    }
}
