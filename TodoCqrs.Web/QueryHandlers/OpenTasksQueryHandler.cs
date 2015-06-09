using System.Collections.Generic;
using System.Linq;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;
using TodoCqrs.Web.Queries;

namespace TodoCqrs.Web.QueryHandlers
{
    public class OpenTasksQueryHandler : IQueryHandler<OpenTasksQuery, IEnumerable<Task>>
    {
        private readonly IRepository repository;

        public OpenTasksQueryHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Task> Handle(OpenTasksQuery query)
        {
            return repository.Query<Task>().Where(t => t.Resolved == false);
        }
    }
}