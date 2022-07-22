using LibraryStore.Models;
using System.Linq.Expressions;

namespace LibraryStore.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task<TEntity> GetById(Guid id);

        Task<IList<TEntity>> GetAll();

        Task Edit(TEntity entity);

        Task Delete(Guid id);

        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
