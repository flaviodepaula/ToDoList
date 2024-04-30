﻿using Domain.Authentication;
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

            var claims = LoadClaimsValues();
            if (claims.Email == null)
                return Unauthorized();

            var result = await _taskDomain.GetAllAsync(claims, cancellationToken);

            return Ok(result.Value);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Tasks.Models.Task>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var claims = LoadClaimsValues();
            if (claims.Email == null)
                return Unauthorized();
                        
            var result = await _taskDomain.GetByIdAsync(id, claims, cancellationToken);

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

            var claims = LoadClaimsValues();
            if (claims.Email == null)
                return Unauthorized();

            var modeloRequisicao = viewModel.ToTaskModel(claims.Email);
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

            var claims = LoadClaimsValues();
            if (claims.Email == null)
                return Unauthorized();

            var result = await _taskDomain.UpdateAsync(modeloRequisicao, claims, cancellationToken);

            return Ok(result);
        }
 
        [NonAction]
        private ClaimsDTO LoadClaimsValues()
        {
            var claims = new ClaimsDTO("","");
            if (HttpContext.User.Identity != null)
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    return claims;
                }
                claims.Email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
                claims.Role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
            }

            return claims;
        }

    }
}
