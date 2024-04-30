using Domain.Tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Extensions;
using WebApi.ViewModel.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskDomain _taskDomain;

        public TaskController(ITaskDomain taskDomain)
        {
            _taskDomain = taskDomain;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            string email = GetEmailFromToken();
            if (email == null)
                return Unauthorized();

            var result = await _taskDomain.GetAllAsync(email, cancellationToken);

            return Ok(result.Value);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Tasks.Models.Task>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            string email = GetEmailFromToken();
            if (email == null)
                return Unauthorized();

            string role = GetRoleFromToken();
            
            var result = await _taskDomain.GetByIdAsync(id, email, role, cancellationToken);

            if (result.IsSucess)
                return Ok(result.Value);

            return BadRequest(result.Error);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<Domain.Tasks.Models.Task>> PostAsync([FromBody] TaskRequestAddViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            string email = GetEmailFromToken();
            if (email == null)
                return Unauthorized();

            var modeloRequisicao = viewModel.ToTaskModel(email);
            var result = await _taskDomain.AddAsync(modeloRequisicao, cancellationToken);

            return Ok(result.Value);
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
        private string GetEmailFromToken()
        { 
            if(HttpContext.User.Identity != null)
                return HttpContext.User.Identity.IsAuthenticated ? HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty : string.Empty;

            return string.Empty;
        }

        [NonAction]
        private string GetRoleFromToken()
        {
            if (HttpContext.User.Identity != null)
                return HttpContext.User.Identity.IsAuthenticated ? HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty : string.Empty;

            return string.Empty;
        }
    }
}
