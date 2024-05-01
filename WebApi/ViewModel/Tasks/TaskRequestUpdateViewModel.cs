using Domain.Tasks.Enums;

namespace WebApi.ViewModel.Tasks
{
    public class TaskRequestUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}
