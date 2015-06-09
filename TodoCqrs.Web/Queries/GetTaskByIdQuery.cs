using TodoCqrs.Web.Bus;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.Queries
{
    public class GetTaskByIdQuery : IQuery<Task>
    {
        public int Id { get; set; }
    }
}