using Domain.Tasks.Enum;
using Infra.Common.Result;

namespace Domain.Tasks.Models
{
    public class Task
    {
        public Guid IdTask { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ToDoStatusEnum Status { get; private set; }
        public DateTime CreationDate { get; }
        public string UserEmail { get; private set; }

        private Task(string titulo, string descricao, ToDoStatusEnum status, string userEmail)
        {
            IdTask = Guid.NewGuid();
            Title = titulo;
            Description = descricao;
            Status = status;
            UserEmail = userEmail;

            CreationDate = DateTime.Now;
        }

        private Task(Guid taskId, string userEmail, string title, string description, ToDoStatusEnum status, DateTime creationDate)
        {
            this.IdTask = taskId;
            Title = title;
            Description = description;
            Status = status;
            UserEmail = userEmail;
            CreationDate = creationDate;
        }

        public static Result<Task> CreateTask(TaskDTO dTO)
        {
            var novaTarefa = new Task(dTO.Title!, dTO.Description!, dTO.Status!, dTO.UserEmail!);

            return novaTarefa;
        }

        public void UpdateTask(string title, string description, ToDoStatusEnum status)
        {
            this.Title = title;
            this.Description = description;
            this.Status = status;
        }

        public static Task LoadTask(TaskDTO dto)
        {
            return new Task(dto.IdTask!,
                              dto.UserEmail!,
                              dto.Title!,
                              dto.Description!,
                              dto.Status!,
                              dto.CreationDate!);
        }
    }
}
