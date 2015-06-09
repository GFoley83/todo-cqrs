namespace TodoCqrs.Web.CommandHandlers
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}