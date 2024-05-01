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

        public static Error GenericErrorOnRetrievingData(string errorMessage, string innerException)
        {
            return new("Repository.Users.Errors.GenericErrorOnRetrievingData", $"An error occurred on request to database. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }

        public static readonly Error UserDoesNotExist = new("Repository.Users.Errors.UserDoesNotExist", $"User required does not exist. ");        
    }
}
