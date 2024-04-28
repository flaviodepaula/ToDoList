using Domain.Authentication.DTO;
using Domain.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using WebApi.ViewModel;

namespace WebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("Login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnprocessableEntityObjectResult), (int)HttpStatusCode.UnprocessableEntity)]
        public async Task<ActionResult> Login(LoginViewModel userViewModel, CancellationToken cancellationToken)
        {

            try
            {
                if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

                var loginDto = new LoginDto(userViewModel.Email, userViewModel.Password);

                var result = await _tokenService.GenerateTokenAsync(loginDto, cancellationToken);
                if (result.IsSucess)
                {                   
                    if (result.Value.IsNullOrEmpty())
                    {
                        return Unauthorized();
                    }

                    return Ok(result.Value);
                }
                else
                {
                    return BadRequest(result.Error);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
