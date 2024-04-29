using Domain.Tasks.Enum;

namespace WebApi.ViewModel.Tasks
{
    public class TaskRequestUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoStatusEnum Status { get; set; }

    }
}
