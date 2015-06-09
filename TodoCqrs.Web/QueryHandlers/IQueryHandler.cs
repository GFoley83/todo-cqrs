using TodoCqrs.Web.Bus;

namespace TodoCqrs.Web.QueryHandlers
{
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}