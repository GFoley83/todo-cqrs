using SimpleInjector;
using TodoCqrs.Web.QueryHandlers;

namespace TodoCqrs.Web.Bus
{
    public class QueryBus : IQueryBus
    {
        private readonly Container container;

        public QueryBus(Container container)
        {
            this.container = container;
        }

        public TResponse Query<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            var handler = container.GetInstance<IQueryHandler<TQuery, TResponse>>();
            return handler.Handle(query);
        }
    }
}