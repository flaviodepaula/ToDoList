using Domain.Tasks.Enum;

namespace WebApi.ViewModel.Tasks
{
    public class TaskRequestAddViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ToDoStatusEnum Status { get; set; }
       
    }
}
