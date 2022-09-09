using LibraryStore.Business.Interfaces;
using LibraryStore.Business.Models.Validations;
using LibraryStore.Models;

namespace LibraryStore.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) 
                return;

            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
                return;

            await _productRepository.Edit(product);
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
