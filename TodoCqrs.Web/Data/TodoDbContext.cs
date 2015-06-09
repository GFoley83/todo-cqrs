using System.Data.Entity;
using TodoCqrs.Web.Models;

namespace TodoCqrs.Web.Data
{
    public class TodoDbContext : DbContext
    {
        public virtual DbSet<Task> Tasks { get; set; }
    }
}