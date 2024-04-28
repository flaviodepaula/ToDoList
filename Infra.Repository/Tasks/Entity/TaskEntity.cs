using Domain.Tasks.Enum;
using Infra.Repository.User.Entities;

namespace Infra.Repository.Tasks.Entity
{
    public record class TaskEntity
    {
        public Guid IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoStatusEnum Status { get; set; }
        public DateTime CreationDate { get; }
        
        public string UserEmail { get; set; }
        public UserEntity User { get; set; }
    }
}
