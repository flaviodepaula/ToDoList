using Domain.Tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Extensions;
using WebApi.ViewModel.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("[controller]")]
    public class TaskController : Controller
    {

        private readonly ITaskDomain _taskDomain;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaskController(ITaskDomain taskDomain, IHttpContextAccessor httpContextAccessor)
        {
            _taskDomain = taskDomain;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            string email = GetUserFromToken();
            if (email == null)
                return Unauthorized();

            var result = await _taskDomain.GetAllAsync(email, cancellationToken);

            return Ok(result);
        }


        [HttpGet("GetById", Name = "GetById")]
        public async Task<IActionResult> GetByIdAsync(Guid IdTarefa, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var result = await _taskDomain.GetByIdAsync(IdTarefa, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> PostAsync([FromBody] TaskRequestAddViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            string email = GetUserFromToken();
            if (email == null)
                return Unauthorized();

            var modeloRequisicao = viewModel.ToTaskModel(email);
            var result = await _taskDomain.AddAsync(modeloRequisicao, cancellationToken);

            return CreatedAtAction(nameof(PostAsync), result.Value);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> PutAsync([FromBody] TaskRequestUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var modeloRequisicao = viewModel.ToTaskModel();

            var result = await _taskDomain.UpdateAsync(modeloRequisicao, cancellationToken);

            return Ok(result);
        }
 

        [NonAction]
        private string GetUserFromToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.User.Identity.IsAuthenticated)
                return httpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? "";

            return string.Empty;
        }

    }
}
