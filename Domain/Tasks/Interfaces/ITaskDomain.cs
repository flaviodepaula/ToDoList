using Domain.Tasks.Models;
using Infra.Common.Result;

namespace Domain.Tasks.Interfaces
{
    public interface ITaskDomain
    {
        Task<Result<TaskDTO>> GetByIdAsync(Guid idTask, string userEmail, string role, CancellationToken cancellationToken);
        Task<Result<IEnumerable<TaskDTO>>> GetAllAsync(string userEmail, CancellationToken cancellationToken);
        Task<Result<Models.Task>> AddAsync(Models.Task requestModel, CancellationToken cancellationToken);
        Task<Result<Models.Task>> UpdateAsync(Models.Task requestModel, CancellationToken cancellationToken);
    }
}
