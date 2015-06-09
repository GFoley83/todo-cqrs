namespace TodoCqrs.Web.Bus
{
    public interface ICommandBus
    {
        void Run<TCommand>(TCommand command);
    }
}