using LibraryStore.Business.Interfaces;
using LibraryStore.Data.Context;
using LibraryStore.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryStore.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(LibraryStoreDbContext context) : base(context) {}

        public async Task<Address> GetAddressByProvider(Guid providerId)
        {
            return await _db.Adresses.AsNoTracking()
                .FirstOrDefaultAsync(a => a.ProviderId == providerId);
        }
    }
}
