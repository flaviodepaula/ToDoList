using Domain.Users.Interfaces;

namespace Infra.Repository.User.Service
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            
        }

        public Task<Domain.Users.Models.User> AddAsync(Domain.Users.Models.User user)
        {
            throw new NotImplementedException();
        }
    }
}
