using Domain.Users.Models;

namespace Domain.Users.Interfaces
{
    public interface IUserDomain
    {
        Task<User> AddAsync(User user);
    }
}
