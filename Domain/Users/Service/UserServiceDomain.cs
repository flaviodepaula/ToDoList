using Domain.Users.Interfaces;
using Domain.Users.Models;

namespace Domain.Users.Service
{
    public class UserServiceDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;
        public UserServiceDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddAsync(User user)
        {
            return await _userRepository.AddAsync(user);            
        }
    }
}
