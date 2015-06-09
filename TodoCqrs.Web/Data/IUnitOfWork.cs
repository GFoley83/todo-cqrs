namespace TodoCqrs.Web.Data
{
    public interface IUnitOfWork
    {
        void Add<T>(T entity) where T : class;
        void Commit();
        void Delete<T>(T entity) where T : class;
    }
}