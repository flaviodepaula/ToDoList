using Domain.Tasks.Enums;
using Infra.Repository.User.Entities;

namespace Infra.Repository.Tasks.Entity
{
    public record class TaskEntity
    {
        public Guid IdTask { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public DateTime CreationDate { get; set; }        
        public string UserEmail { get; set; }
        public UserEntity User { get; set; }
    }
}
