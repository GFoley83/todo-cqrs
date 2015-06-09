using System.Linq;

namespace TodoCqrs.Web.Data
{
    internal class DbContextRepository : IRepository
    {
        private readonly TodoDbContext dbContext;

        public DbContextRepository(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public T GetById<T>(int id) where T : class
        {
            return dbContext.Set<T>().Find(id);
        }
    }
}