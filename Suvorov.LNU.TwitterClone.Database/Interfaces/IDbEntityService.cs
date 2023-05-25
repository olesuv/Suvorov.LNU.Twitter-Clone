using Suvorov.LNU.TwitterClone.Database;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Database.Interfaces
{
    public interface IDbEntityService<T> where T : DbItem
    {
        Task<T> GetById(int id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);

        IQueryable<T> GetAll();
    }
}
