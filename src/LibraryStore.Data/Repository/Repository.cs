using LibraryStore.Business.Interfaces;
using LibraryStore.Data.Context;
using LibraryStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryStore.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly LibraryStoreDbContext _db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(LibraryStoreDbContext db)
        {
            _db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);

            await SaveChanges();
        }

        public virtual async Task Edit(TEntity entity)
        {
            DbSet.Update(entity);

            await SaveChanges();
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = new TEntity { Id = id };

            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
