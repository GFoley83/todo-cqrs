namespace TodoCqrs.Web.Bus
{
    public interface IQueryBus
    {
        TResponse Query<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}