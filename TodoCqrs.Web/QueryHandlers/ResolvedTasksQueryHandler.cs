using System.Collections.Generic;
using System.Linq;
using TodoCqrs.Web.Data;
using TodoCqrs.Web.Models;
using TodoCqrs.Web.Queries;

namespace TodoCqrs.Web.QueryHandlers
{
    public class ResolvedTasksQueryHandler : IQueryHandler<ResolvedTasksQuery, IEnumerable<Task>>
    {
        private readonly IRepository repository;

        public ResolvedTasksQueryHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Task> Handle(ResolvedTasksQuery query)
        {
            return repository.Query<Task>().Where(t => t.Resolved);
        }
    }
}