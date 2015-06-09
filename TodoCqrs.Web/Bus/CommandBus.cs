using SimpleInjector;
using TodoCqrs.Web.CommandHandlers;

namespace TodoCqrs.Web.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly Container container;

        public CommandBus(Container container)
        {
            this.container = container;
        }

        public void Run<TCommand>(TCommand command)
        {
            var handler = container.GetInstance<ICommandHandler<TCommand>>();
            handler.Handle(command);
        }
    }
}