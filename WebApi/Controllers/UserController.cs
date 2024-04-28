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
        public async Task<ActionResult> AddUser(UserDto userDto, CancellationToken cancellationToken)
        {
            var result = await _userDomain.AddAsync(userDto.ToUserModel(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("GetAll", Name = "GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllUser(CancellationToken cancellationToken)
        {
            var result = await _userDomain.GetAllAsync(cancellationToken);

            return Ok(result);
        }

    }
}
