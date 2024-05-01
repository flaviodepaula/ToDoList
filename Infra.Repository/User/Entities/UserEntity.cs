using Infra.Repository.Tasks.Entity;

namespace Infra.Repository.User.Entities
{
    public class UserEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public ICollection<TaskEntity>? Tasks { get; set; }
    }
}
