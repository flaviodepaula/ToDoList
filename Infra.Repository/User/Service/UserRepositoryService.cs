using Domain.Users.Interfaces;
using Infra.Common.Result;
using Infra.Repository.Context;
using Infra.Repository.User.Entities;
using Infra.Repository.User.Errors;
using Infra.Repository.User.Extensions;
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

        public async Task<Result<Domain.Users.Models.User>> AddAsync(Domain.Users.Models.User user, CancellationToken cancellationToken)
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

                    return Result.Sucess(user);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Failure<Domain.Users.Models.User>(UserRepositoryErrors.UnableToCreateUser(ex.Message, ex.InnerException?.ToString() ?? ""));
                }
            }            
        }

        public async Task<Result<IEnumerable<Domain.Users.Models.User>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {

                var users = await _databaseContext.Users.ToListAsync(cancellationToken);


                var result = users.Select(x => new Domain.Users.Models.User() { 
                                                    Email = x.Email, 
                                                    Id = x.Id,
                                                    Password = x.Password,
                                                    Role = x.Role,
                                                    UserName = x.UserName});

                return Result.Sucess(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Domain.Users.Models.User>>(UserRepositoryErrors.UnableToGetUsers(ex.Message, ex.InnerException?.ToString() ?? ""));
            }
        }

        public async Task<Result<Domain.Users.Models.User>> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _databaseContext.Users.FirstOrDefaultAsync(x=> x.Email.Equals(email), cancellationToken);

                if (user == null)
                    return Result.Failure<Domain.Users.Models.User>(UserRepositoryErrors.UserDoesNotExist);

                return Result.Sucess(user!.ToDomainModel());
            }
            catch (Exception ex)
            {
                return Result.Failure<Domain.Users.Models.User>(UserRepositoryErrors.GenericErrorOnRetrievingData(ex.Message, ex.InnerException?.ToString() ?? ""));
            }
        }
    }
}
