using LibraryStore.Models;

namespace LibraryStore.Business.Interfaces
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAddress(Guid id);

        Task<Provider> GetProviderProductsAddress(Guid id);
    }
}
