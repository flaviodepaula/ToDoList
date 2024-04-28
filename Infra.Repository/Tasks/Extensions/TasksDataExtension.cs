using Domain.Tasks.Models;
using Infra.Repository.Tasks.Entity;

namespace Infra.Repository.Tasks.Extensions
{
    public static class TasksDataExtension
    {
        public static TaskDTO ToTasksDTO(this TaskEntity tarefa)
        {
            return new TaskDTO()
            {
                CreationDate = tarefa.CreationDate,
                Description = tarefa.Description,
                IdTask = tarefa.IdTask,
                UserEmail = tarefa.UserEmail,
                Status = tarefa.Status,
                Title = tarefa.Title
            };
        }
    }
}
