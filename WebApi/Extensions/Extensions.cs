using Domain.Users.Models;
using WebApi.ViewModel.Tasks;
using WebApi.ViewModel.Users;

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

        public static Domain.Tasks.Models.Task ToTaskModel(this TaskRequestAddViewModel requestAddViewModel, string userEmail)
        {
            var task = new Domain.Tasks.Models.Task()
            {                
                Description = requestAddViewModel.Description,
                Status = requestAddViewModel.Status,
                Title = requestAddViewModel.Title,
                UserEmail = userEmail
            };
             
            return task;
        }

        public static Domain.Tasks.Models.Task ToTaskModel(this TaskRequestUpdateViewModel requestUpdateViewModel)
        {
            var task = new Domain.Tasks.Models.Task()
            {
                IdTask = requestUpdateViewModel.Id,
                Description = requestUpdateViewModel.Description,
                Status = requestUpdateViewModel.Status,
                Title = requestUpdateViewModel.Title,
            };

            return task;
        }
    }
}
