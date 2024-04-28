using Domain.Users.Interfaces;
using Domain.Users.Models;

namespace Domain.Users.Service
{
    public class UserDomainService : IUserDomain
    {
        private readonly IUserRepository _userRepository;
        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
        {
            user.Id = Guid.NewGuid();

            return await _userRepository.AddAsync(user, cancellationToken);
        }
    }
}
