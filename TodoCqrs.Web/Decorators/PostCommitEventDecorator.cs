using TodoCqrs.Web.CommandHandlers;
using TodoCqrs.Web.Infrastructure;

namespace TodoCqrs.Web.Decorators
{
    public class PostCommitEventDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> decorated;
        private readonly PostCommitEvent postCommitEvent;

        public PostCommitEventDecorator(ICommandHandler<TCommand> decorated, PostCommitEvent postCommitEvent)
        {
            this.decorated = decorated;
            this.postCommitEvent = postCommitEvent;
        }

        public void Handle(TCommand command)
        {
            try
            {
                decorated.Handle(command);
                postCommitEvent.Raise();
            }
            finally
            {
                postCommitEvent.Reset();
            }
        }
    }
}