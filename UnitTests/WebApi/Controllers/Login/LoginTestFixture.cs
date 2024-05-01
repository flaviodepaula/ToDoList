using Domain.Authentication.Interfaces;
using Moq;
using WebApi.ViewModel.Login;

namespace UnitTests.WebApi.Controllers.Login
{
    [CollectionDefinition(nameof(LoginTestFixture))]
    public class LoginTestFixtureCollection : ICollectionFixture<LoginTestFixture>
    { }

    public class LoginTestFixture
    {
        public Mock<ITokenService> GetTokenServiceMock() => new();

        public LoginViewModel GetLoginViewModelMock() => new LoginViewModel(){
            Email = "teste@teste.com.br",
            Password = "password"
        };

        public string GenerateTokenMock() => "token";
        public string GenerateTokenVazio() => "";

        

    }
}
