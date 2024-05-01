using Infra.Common.Errors;

namespace Infra.Repository.User.Errors
{
    public static class UserRepositoryErrors
    {   
        public static Error UnableToCreateUser(string errorMessage, string innerException)
        {
            return new("Repository.Users.Errors.UnableToCreateUser", $"Não foi possível criar o Usuário. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }

        public static Error UnableToGetUsers(string errorMessage, string innerException)
        {
            return new("Repository.Users.Errors.UnableToGetUsers", $"Não foi possível recueprar as informações dos usuários. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }

        public static Error GenericErrorOnRetrievingData(string errorMessage, string innerException)
        {
            return new("Repository.Users.Errors.GenericErrorOnRetrievingData", $"Erro ao efetuar requisição para o banco de dados. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }

        public static readonly Error UserDoesNotExist = new("Repository.Users.Errors.UserDoesNotExist", $"Usuário pesquisado não existe. ");        
    }
}
