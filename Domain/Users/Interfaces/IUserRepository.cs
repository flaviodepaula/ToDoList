using Domain.Users.Models;

namespace Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
    }
}
