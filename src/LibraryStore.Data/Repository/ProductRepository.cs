using LibraryStore.Business.Interfaces;
using LibraryStore.Data.Context;
using LibraryStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryStore.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(LibraryStoreDbContext context) : base(context){ }

        public async Task<Product> GetProductProvider(Guid id)
        {
            return await _db.Products
                .AsNoTracking()
                .Include(prod => prod.Provider)
                .FirstOrDefaultAsync(prod => prod.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await _db.Products
              .AsNoTracking()
              .Include(prod => prod.Provider)
              .OrderBy(prod => prod.Name)
              .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByProvider(Guid providerId)
        {
            return await Search(prod => prod.ProviderId == providerId);
        }
    }
}
