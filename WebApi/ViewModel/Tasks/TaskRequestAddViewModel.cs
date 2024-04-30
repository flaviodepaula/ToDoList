using Domain.Tasks.Enums;

namespace WebApi.ViewModel.Tasks
{
    public class TaskRequestAddViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum Status { get; set; }
       
    }
}
