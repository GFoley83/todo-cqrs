using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TodoCqrs.Web.Data
{
    public class TodoDbConfiguration : DbConfiguration
    {
        public TodoDbConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory("server=localhost;integrated security=true"));
            SetDatabaseInitializer(new TodoDatabaseInitializer());
        }
    }
}