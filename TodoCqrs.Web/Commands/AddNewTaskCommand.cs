namespace TodoCqrs.Web.Commands
{
    public class AddNewTaskCommand
    {
        public string Text { get; set; }
        public int CreatedTaskId { get; set; }
    }
}