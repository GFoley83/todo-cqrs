using System.Linq;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;
using TodoCqrs.Web.Queries;

namespace TodoCqrs.Web.QueryHandlers
{
    public class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, Task>
    {
        private readonly IRepository repository;

        public GetTaskByIdQueryHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public Task Handle(GetTaskByIdQuery query)
        {
            return repository.Query<Task>().Single(t => t.Id == query.Id);
        }
    }
}