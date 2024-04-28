using Domain.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Extensions;

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

        [HttpPost("AddUser", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> AddUser(UserRequestDto userDto, CancellationToken cancellationToken)
        {
           
            try
            {
                var result = await _userDomain.AddAsync(userDto.ToUserModel(), cancellationToken);
                if (result.IsSucess)
                {
                    var newUser = result.Value.ToUserResponseDto();
                    return Ok(newUser);
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

        [HttpGet("GetAll", Name = "GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> GetAllUser(CancellationToken cancellationToken)
        {
            try
            {
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
                throw;
            }
        }

    }
}
