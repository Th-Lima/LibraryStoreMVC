using LibraryStore.Business.Interfaces;
using LibraryStore.Business.Models.Validations;
using LibraryStore.Models;

namespace LibraryStore.Business.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(IProviderRepository providerRepository, IAddressRepository addressRepository, INotifier notifier) : base(notifier)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }

        public async Task Add(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider) || !ExecuteValidation(new AddressValidation(), provider.Address)) 
                 return;

            if(_providerRepository.Search(p => p.Document == provider.Document).Result.Any())
            {
                Notification("Já existe um fornecedor com este documento informado!");
                return;
            }

            await _providerRepository.Add(provider);
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider))
                return;

            if(_providerRepository.Search(p => p.Document == provider.Document && p.Id != provider.Id).Result.Any())
            {
                Notification("Já existe um fornecedor com este documento informado!");
                return;
            }

            await _providerRepository.Edit(provider);
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
                return;

            await _addressRepository.Edit(address);
        }

        public async Task Remove(Guid id)
        {
            if (_providerRepository.GetProviderProductsAddress(id).Result.Products.Any())
            {
                Notification("O fornecedor possui produtos cadastrados");
            }

            await _providerRepository.Delete(id);
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
