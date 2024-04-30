using Domain.Authentication;
using Domain.Tasks.Enums;
using Domain.Tasks.Errors;
using Domain.Tasks.Interfaces;
using Domain.Tasks.Models;
using Infra.Common.Result;

namespace Domain.Tasks.Service
{
    public class TaskDomainService : ITaskDomain
    {
        private readonly ITaskRepository _taskRepository;

        public TaskDomainService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<Models.Task>> AddAsync(Models.Task requestModel, CancellationToken cancellationToken)
        {
            requestModel.IdTask = Guid.NewGuid();
            requestModel.CreationDate = DateTime.Now;

            return await _taskRepository.AddAsync(requestModel, cancellationToken);
        }

        public async Task<Result<IEnumerable<TaskDTO>>> GetAllAsync(ClaimsDTO claims, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Result<IEnumerable<TaskDTO>>> GetAllByEmailAsync(ClaimsDTO claims, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAllByEmailAsync(claims.Email, cancellationToken);
        }

        public async Task<Result<TaskDTO>> GetByIdAsync(Guid idTask, ClaimsDTO claims, CancellationToken cancellationToken)
        {            
            var result = await _taskRepository.GetByIdAsync(idTask, cancellationToken);

            if (result.IsFailure)
                return Result.Failure<TaskDTO>(result.Error);

            _ = Enum.TryParse(claims.Role, out Roles role);
            if(role != Roles.Admin && result.Value.UserEmail != claims.Email)
                return Result.Failure<TaskDTO>(TasksDomainErrors.TaskNaoPertenceAoEmail);
            
            return result;
        }

        public async Task<Result<Models.Task>> UpdateAsync(Models.Task requestModel, ClaimsDTO claims, CancellationToken cancellationToken)
        {
            return await _taskRepository.UpdateAsync(requestModel, cancellationToken);
        }
    }
}
