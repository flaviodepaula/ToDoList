using Domain.Authentication.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace UnitTests.WebApi.Controllers.Login
{
    [Collection(nameof(LoginTestFixture))]
    public class LoginTest
    {
        private readonly LoginTestFixture _fixture;

        public LoginTest(LoginTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Trait("WebApi", "Login - Controllers")]
        [Fact]
        public async Task Login_LoginComSucessoRetornaValidaToken()
        {
            // Arrange
            var tokenService = _fixture.GetTokenServiceMock();
            var loginModel = _fixture.GetLoginViewModelMock();
            var exampleResponse = _fixture.GenerateTokenMock();

            tokenService.Setup(x => x.GenerateTokenAsync(It.IsAny<LoginDto>(), CancellationToken.None)).ReturnsAsync(exampleResponse);

            var controller = new LoginController(tokenService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            controller.ControllerContext.HttpContext.Request.Method = HttpMethods.Post;

           // Act
           var result = await controller.Login(loginModel, CancellationToken.None);

            // Asert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("token", okResult.Value);
        }

        [Trait("WebApi", "Login - Controllers")]
        [Fact]
        public async Task Login_LoginComFalhaRetornaUnauthorized()
        {
            // Arrange
            var tokenService = _fixture.GetTokenServiceMock();
            var loginModel = _fixture.GetLoginViewModelMock();
            var exampleResponse = _fixture.GenerateTokenVazio();

            tokenService.Setup(x => x.GenerateTokenAsync(It.IsAny<LoginDto>(), CancellationToken.None)).ReturnsAsync(exampleResponse);

            var controller = new LoginController(tokenService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            controller.ControllerContext.HttpContext.Request.Method = HttpMethods.Post;

            // Act
            var result = await controller.Login(loginModel, CancellationToken.None);

            // Asert
            Assert.IsType<UnauthorizedResult>(result);
        }


    }
}