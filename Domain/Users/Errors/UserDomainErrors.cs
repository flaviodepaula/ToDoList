using Infra.Common.Errors;

namespace Domain.Users.Errors
{
    public static class UserDomainErrors
    {
        public static readonly Error RequiredPassword = new("Domain.Users.Errors.RequiredPassword", $"Password is required. ");
        public static readonly Error RequiredEmail = new("Domain.Users.Errors.RequiredPassword", $"Email is required. ");
        public static readonly Error RequiredUserName = new("Domain.Users.Errors.RequiredPassword", $"UserName is required. ");
        public static readonly Error InvalidPassword = new("Domain.Users.Errors.WrongPassword", $"Invalid Password, plese try again. ");

        public static readonly Error InvalidConfigurations = new("Domain.Users.Errors.InvalidConfigurations", $"Invalid Configurations, plese verify it. ");

        public static Error ErrorOnTokenGeneration(string errorMessage, string innerException)
        {
            return new("Domain.Users.Errors.ErrorOnTokenGeneration", $"An error occurred on generating the token. ErrorMessage: {errorMessage}. InnerException: {innerException}");
        }
    }
}
