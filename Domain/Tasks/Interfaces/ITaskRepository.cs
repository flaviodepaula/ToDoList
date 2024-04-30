using Domain.Tasks.Models;
using Infra.Common.Result;
 

namespace Domain.Tasks.Interfaces
{
    public interface ITaskRepository
    {
        Task<Result<TaskDTO>> GetByIdAsync(Guid idTask, CancellationToken cancellationToken);
        Task<Result<IEnumerable<TaskDTO>>> GetAllByEmailAsync(string userEmail, CancellationToken cancellationToken);
        Task<Result<IEnumerable<TaskDTO>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<Models.Task>> AddAsync(Models.Task modeloRequest, CancellationToken cancellationToken);
        Task<Result<Models.Task>> UpdateAsync(Models.Task modeloRequest, CancellationToken cancellationToken);

    }
}
