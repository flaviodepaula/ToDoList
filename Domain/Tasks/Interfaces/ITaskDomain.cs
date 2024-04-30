using Domain.Authentication;
using Domain.Tasks.Models;
using Infra.Common.Result;

namespace Domain.Tasks.Interfaces
{
    public interface ITaskDomain
    {
        Task<Result<TaskDTO>> GetByIdAsync(Guid idTask, ClaimsDTO claims, CancellationToken cancellationToken);
        Task<Result<IEnumerable<TaskDTO>>> GetAllByEmailAsync(ClaimsDTO claims, CancellationToken cancellationToken);
        Task<Result<IEnumerable<TaskDTO>>> GetAllAsync(ClaimsDTO claims, CancellationToken cancellationToken);
        Task<Result<Models.Task>> AddAsync(Models.Task requestModel, CancellationToken cancellationToken);
        Task<Result<Models.Task>> UpdateAsync(Models.Task requestModel, ClaimsDTO claims, CancellationToken cancellationToken);
    }
}
