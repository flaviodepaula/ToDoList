using Infra.Common.Errors;

namespace Infra.Repository.User.Errors
{
    public static class UserRepositoryErrors
    {   
        public static Error UnableToCreateUser(string errorMessage, string innerException)
        {
            return new("Repository.Users.Errors.UnableToCreateUser", $"Unable to create User. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }

        public static Error UnableToGetUsers(string errorMessage, string innerException)
        {
            return new("Repository.Users.Errors.UnableToGetUsers", $"Unable to get Users. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }


    }
}
