namespace TodoCqrs.Web.Data
{
    internal class DbContextUnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext dbContext;

        public DbContextUnitOfWork(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            dbContext.Set<T>().Remove(entity);
        }
    }
}