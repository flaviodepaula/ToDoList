using Infra.Common.Errors;

namespace WebApi.Errors
{
    public static class UserWebApiErrors
    {
        public static Error GenericError(string errorMessage, string innerException)
        {
            return new("WebApi.Users.Errors.GenericError", $"Erro ocorreu na requisição, favor tentar novamente. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }
    }
}
