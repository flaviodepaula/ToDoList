using Domain.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using WebApi.ViewModel.Users;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserDomain _userDomain;

        public UserController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserResponseViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UnprocessableEntityObjectResult), (int)HttpStatusCode.UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddAsync(UserRequestViewModel userDto, CancellationToken cancellationToken)
        {           
            try
            {
                if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

                var result = await _userDomain.AddAsync(userDto.ToUserModel(), cancellationToken);
                if (result.IsSucess)
                {                    
                    return StatusCode(201);
                }
                else
                {
                    return BadRequest(result.Error);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, UserWebApiErrors.GenericError(ex.Message, ex.InnerException.ToString()));
            }
              
        }

        [HttpGet(Name = "GetAll")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

                var result = await _userDomain.GetAllAsync(cancellationToken);

                if (result.IsSucess)
                {
                    var resultModel = result.Value.ToUserResponseDto();

                    return Ok(resultModel);
                }
                else
                {
                    return BadRequest(result.Error);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, UserWebApiErrors.GenericError(ex.Message, ex.InnerException.ToString()));
            }
        }

    }
}
