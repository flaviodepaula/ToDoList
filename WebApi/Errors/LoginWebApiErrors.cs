using Infra.Common.Errors;

namespace WebApi.Errors.WebApi
{
    public static class LoginWebApiErrors
    {
        public static Error GenericError(string errorMessage, string innerException)
        {
            return new("WebApi.Task.Errors.GenericError", $"Erro ocorreu na requisição, favor tentar novamente. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }
    }
}
