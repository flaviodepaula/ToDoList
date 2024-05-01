using Infra.Common.Errors;

namespace Domain.Users.Errors
{
    public static class UserDomainErrors
    {
        public static readonly Error RequiredPassword = new("Domain.Users.Errors.RequiredPassword", $"Password obrigatório. ");
        public static readonly Error RequiredEmail = new("Domain.Users.Errors.RequiredPassword", $"Email obrigatório. ");
        public static readonly Error RequiredUserName = new("Domain.Users.Errors.RequiredPassword", $"UserName obrigatório. ");
        public static readonly Error InvalidPassword = new("Domain.Users.Errors.WrongPassword", $"Password invalido, favor tentar novamente. ");

        public static readonly Error InvalidConfigurations = new("Domain.Users.Errors.InvalidConfigurations", $"COnfigurações inválidas, favor verificar. ");

        public static Error ErrorOnTokenGeneration(string errorMessage, string innerException)
        {
            return new("Domain.Users.Errors.ErrorOnTokenGeneration", $"Ocorreu um erro na geração do Token. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }
    }
}
