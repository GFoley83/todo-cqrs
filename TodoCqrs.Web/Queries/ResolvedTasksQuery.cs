using System.Collections.Generic;
using TodoCqrs.Web.Bus;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.Queries
{
    public class ResolvedTasksQuery : IQuery<IEnumerable<Task>>
    {
    }
}