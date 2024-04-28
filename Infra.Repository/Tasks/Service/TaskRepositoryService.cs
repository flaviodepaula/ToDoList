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
                        return Result.Failure<Domain.Tasks.Models.Task>(TasksErrors.InfoDoesNotExist);

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
                    return Result.Failure<Domain.Tasks.Models.Task>(TasksErrors.UnableToCreateTask(ex.Message, ex.InnerException?.ToString() ?? ""));
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
                    return Result.Failure<Domain.Tasks.Models.Task>(TasksErrors.UnableToCreateTask(ex.Message, ex.InnerException?.ToString() ?? ""));
                }
            }
        }
   
        public async Task<Result<TaskDTO>> GetByIdAsync(Guid idTarefa, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _databaseContext.Tasks.FirstOrDefaultAsync(x => x.IdTask.Equals(idTarefa), cancellationToken);

                if (result == null)
                    return Result.Failure<TaskDTO>(TasksErrors.InfoDoesNotExist);

                return Result.Sucess(result.ToTasksDTO());
            }
            catch (Exception ex)
            {
                return Result.Failure<TaskDTO>(TasksErrors.RequestToDatabaseFailed(ex.Message));
            }
        }

        public async Task<Result<IEnumerable<TaskDTO>>> GetAllAsync(string userEmail, CancellationToken cancellationToken)
        {
            try
            {
                var tarefas = await _databaseContext.Tasks.Where(p => p.UserEmail.Equals(userEmail)).ToListAsync(cancellationToken);

                List<TaskDTO> ListaTarefas = new List<TaskDTO>();

                if (tarefas.Count != 0)
                {
                    foreach (var tarefa in tarefas)
                    {
                        ListaTarefas.Add(new TaskDTO()
                        {
                            IdTask = tarefa.IdTask,
                            CreationDate = tarefa.CreationDate,
                            Description = tarefa.Description,
                            UserEmail = tarefa.UserEmail,
                            Status = tarefa.Status,
                            Title = tarefa.Title
                        });
                    }
                }

                return Result.Sucess(ListaTarefas.AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<TaskDTO>>(TasksErrors.RequestToDatabaseFailed(ex.Message));
            }
        }
    }
}
