using TodoCqrs.Web.Commands;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.CommandHandlers
{
    public class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
    {
        private readonly IRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteTaskCommandHandler(IRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Handle(DeleteTaskCommand command)
        {
            var task = repository.GetById<Task>(command.Id);
            unitOfWork.Delete(task);
        }
    }
}