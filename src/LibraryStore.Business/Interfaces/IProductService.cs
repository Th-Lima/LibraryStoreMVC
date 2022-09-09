using LibraryStore.Models;

namespace LibraryStore.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);

        Task Update(Product product);

        Task Remove(Guid id);
    }
}
