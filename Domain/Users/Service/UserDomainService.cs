using Domain.Users.Interfaces;
using Domain.Users.Models;
using Infra.Common.Result;

namespace Domain.Users.Service
{
    public class UserDomainService : IUserDomain
    {
        private readonly IUserRepository _userRepository;
        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> AddAsync(User user, CancellationToken cancellationToken)
        {
            user.Id = Guid.NewGuid();

            var result = await _userRepository.AddAsync(user, cancellationToken);

            if (result.IsFailure)
                return Result.Failure<User>(result.Error);

            return result.Value;
        }

        public async Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllAsync(cancellationToken);

            if (result.IsFailure)
                return Result.Failure<IEnumerable<User>>(result.Error);

            return result.Value.ToList();

        }
    }
}
