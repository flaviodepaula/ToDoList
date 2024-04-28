using Domain.Users.Interfaces;
using Infra.Repository.Context;
using Infra.Repository.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.User.Service
{
    public class UserRepositoryService : IUserRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepositoryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Domain.Users.Models.User> AddAsync(Domain.Users.Models.User user, CancellationToken cancellationToken)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                try
                {
                    var userEntity = new UserEntity()
                    {
                        Email = user.Email,
                        Password = user.Password,
                        Id = user.Id,
                        Role = user.Role,
                        UserName = user.UserName
                    };

                    await _databaseContext.Users.AddAsync(userEntity, cancellationToken);
                    await _databaseContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return user;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }            
        }

        public async Task<IEnumerable<Domain.Users.Models.User>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {

                var users = await _databaseContext.Users.ToListAsync(cancellationToken);


                return users.Select(x => new Domain.Users.Models.User() { 
                                                    Email = x.Email, 
                                                    Id = x.Id,
                                                    Password = x.Password,
                                                    Role = x.Role,
                                                    UserName = x.UserName});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
