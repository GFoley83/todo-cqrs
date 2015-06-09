using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Infrastructure;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.CommandHandlers
{
    public class AddNewTaskCommandHandler : ICommandHandler<AddNewTaskCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPostCommitEvent postCommitEvent;

        public AddNewTaskCommandHandler(IUnitOfWork unitOfWork, IPostCommitEvent postCommitEvent)
        {
            this.unitOfWork = unitOfWork;
            this.postCommitEvent = postCommitEvent;
        }

        public void Handle(AddNewTaskCommand command)
        {
            var newTask = new Task
            {
                Text = command.Text
            };

            unitOfWork.Add(newTask);

            postCommitEvent.PostCommit += () => command.CreatedTaskId = newTask.Id;
        }
    }
}