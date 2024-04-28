using Domain.Users.Models;
using Infra.Common.Result;

namespace Domain.Users.Interfaces
{
    public interface IUserDomain
    {
        Task<Result<User>> AddAsync(User user, CancellationToken cancellationToken);
        Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken);
    }
}
