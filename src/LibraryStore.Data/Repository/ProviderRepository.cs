using LibraryStore.Business.Interfaces;
using LibraryStore.Data.Context;
using LibraryStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryStore.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(LibraryStoreDbContext context) : base(context){ }

        public async Task<Provider> GetProviderAddress(Guid id)
        {
            return await _db.Providers.AsNoTracking()
                .Include(prov => prov.Address)
                .FirstOrDefaultAsync(prov => prov.Id == id);
        }

        public async Task<Provider> GetProviderProductsAddress(Guid id)
        {
            return await _db.Providers.AsNoTracking()
              .Include(prov => prov.Products)
              .Include(prov => prov.Address)
              .FirstOrDefaultAsync(prov => prov.Id == id);
        }
    }
}
