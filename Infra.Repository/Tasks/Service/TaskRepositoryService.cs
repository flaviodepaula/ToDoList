using Domain.Tasks.Enums;
using Domain.Tasks.Interfaces;
using Domain.Tasks.Models;
using Infra.Common.Result;
using Infra.Repository.Context;
using Infra.Repository.Tasks.Entity;
using Infra.Repository.Tasks.Errors;
using Infra.Repository.Tasks.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.Tasks.Service
{
    internal class TaskRepositoryService : ITaskRepository
    {
        private readonly DatabaseContext _databaseContext;
        public TaskRepositoryService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Result<Domain.Tasks.Models.Task>> UpdateAsync(Domain.Tasks.Models.Task modeloRequest, CancellationToken cancellationToken)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                try
                {
                    var taskDto = await _databaseContext.Tasks.FindAsync(modeloRequest.IdTask, cancellationToken);
                    if(taskDto == null) 
                        return Result.Failure<Domain.Tasks.Models.Task>(TasksRepositoryErrors.InfoDoesNotExist);

                    taskDto.Description = modeloRequest.Description;
                    taskDto.Title = modeloRequest.Title;
                    taskDto.Status = modeloRequest.Status;

                    await _databaseContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return Result.Sucess(modeloRequest);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Failure<Domain.Tasks.Models.Task>(TasksRepositoryErrors.UnableToCreateTask(ex.Message, ex.InnerException?.ToString() ?? ""));
                }
            }
        }

        public async Task<Result<Domain.Tasks.Models.Task>> AddAsync(Domain.Tasks.Models.Task modeloRequest, CancellationToken cancellationToken)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                try
                {
                    var taskEntity = new TaskEntity()
                    {
                        Description = modeloRequest.Description,
                        UserEmail = modeloRequest.UserEmail,
                        Title = modeloRequest.Title,
                        Status = modeloRequest.Status,
                        CreationDate = modeloRequest.CreationDate,
                        IdTask = modeloRequest.IdTask
                    };

                    await _databaseContext.Tasks.AddAsync(taskEntity, cancellationToken);
                    await _databaseContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return Result.Sucess(modeloRequest);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result.Failure<Domain.Tasks.Models.Task>(TasksRepositoryErrors.UnableToCreateTask(ex.Message, ex.InnerException?.ToString() ?? ""));
                }
            }
        }
   
        public async Task<Result<TaskDTO>> GetByIdAsync(Guid idTask, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _databaseContext.Tasks.FirstOrDefaultAsync(x => x.IdTask.Equals(idTask), cancellationToken);

                if (result == null)
                    return Result.Failure<TaskDTO>(TasksRepositoryErrors.InfoDoesNotExist);

                return Result.Sucess(result.ToTasksDTO());
            }
            catch (Exception ex)
            {
                return Result.Failure<TaskDTO>(TasksRepositoryErrors.RequestToDatabaseFailed(ex.Message));
            }
        }

        public async Task<Result<IEnumerable<TaskDTO>>> GetAllByEmailAsync(string userEmail, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _databaseContext.Tasks.Where(p => p.UserEmail.Equals(userEmail)).OrderBy(x=> x.UserEmail).ToListAsync(cancellationToken);

                return LoadData(tasks);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<TaskDTO>>(TasksRepositoryErrors.RequestToDatabaseFailed(ex.Message));
            }
        }

        public async Task<Result<IEnumerable<TaskDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _databaseContext.Tasks.OrderBy(x => x.UserEmail).ToListAsync(cancellationToken);

                return LoadData(tasks);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<TaskDTO>>(TasksRepositoryErrors.RequestToDatabaseFailed(ex.Message));
            }
        }

        private Result<IEnumerable<TaskDTO>> LoadData(List<TaskEntity>? taskEntities)
        {
            List<TaskDTO> ListaTarefas = [];

            if (taskEntities!.Count != 0)
            {
                foreach (var task in taskEntities)
                {                    
                    ListaTarefas.Add(new TaskDTO()
                    {
                        IdTask = task.IdTask,
                        CreationDate = task.CreationDate,
                        Description = task.Description,
                        UserEmail = task.UserEmail,
                        Status = task.Status,
                        Title = task.Title
                    });
                }
            }

            return Result.Sucess(ListaTarefas.AsEnumerable());
        }
    }
}
