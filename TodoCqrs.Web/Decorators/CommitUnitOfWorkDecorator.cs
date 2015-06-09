using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Data;

namespace TodoCqrs.Web.Decorators
{
    public class CommitUnitOfWorkDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;
        private readonly IUnitOfWork unitOfWork;

        public CommitUnitOfWorkDecorator(ICommandHandler<TCommand> decorated, IUnitOfWork unitOfWork)
        {
            this.decorated = decorated;
            this.unitOfWork = unitOfWork;
        }

        public void Handle(TCommand command)
        {
            decorated.Handle(command);
            unitOfWork.Commit();
        }
    }
}