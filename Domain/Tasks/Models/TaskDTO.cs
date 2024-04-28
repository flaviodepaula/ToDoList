using Domain.Tasks.Enum;

namespace Domain.Tasks.Models
{
    public record TaskDTO
    {
        public Guid IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoStatusEnum Status { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserEmail { get; set; }
    }
}
