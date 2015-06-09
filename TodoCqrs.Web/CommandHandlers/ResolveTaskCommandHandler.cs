using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.CommandHandlers
{
    public class ResolveTaskCommandHandler : ICommandHandler<ResolveTaskCommand>
    {
        private readonly IRepository repository;

        public ResolveTaskCommandHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(ResolveTaskCommand command)
        {
            var task = repository.GetById<Task>(command.Id);
            task.Resolved = true;
        }
    }
}