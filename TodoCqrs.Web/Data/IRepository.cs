using System.Linq;

namespace TodoCqrs.Web.Data
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T : class;
        T GetById<T>(int id) where T : class;
    }
}