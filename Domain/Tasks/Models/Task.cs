using Domain.Tasks.Enum;

namespace Domain.Tasks.Models
{
    public class Task
    {
        public Guid IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoStatusEnum Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserEmail { get; set; }

        public Task(string titulo, string descricao, ToDoStatusEnum status, string userEmail)
        {
            IdTask = Guid.NewGuid();
            Title = titulo;
            Description = descricao;
            Status = status;
            UserEmail = userEmail;

            CreationDate = DateTime.Now;
        }

        public Task()
        {
            
        }        
    }
}
