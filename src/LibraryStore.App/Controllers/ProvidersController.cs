using Microsoft.AspNetCore.Mvc;
using LibraryStore.App.ViewModels;
using LibraryStore.Business.Interfaces;
using AutoMapper;
using LibraryStore.Models;
using Microsoft.AspNetCore.Authorization;
using LibraryStore.App.Extensions;

namespace LibraryStore.App.Controllers
{
    [Authorize]
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderRepository context,
            IMapper mapper,
            IProviderService providerService,
            INotifier notifier) : base(notifier)
        {
            _providerRepository = context;
            _providerService = providerService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll()));
        }

        [AllowAnonymous]
        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Add")]
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Provider", "Add")]
        [Route("novo-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid) 
                return View(providerViewModel);
                
            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Add(provider);

            if (!ValidOperation())
                return View(providerViewModel);

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Provider", "Update")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductAddress(id);
           
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Update")]
        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Update(provider);

            if (!ValidOperation())
                return View(await GetProviderProductAddress(id));

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Provider", "Remove")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
           
            if (providerViewModel == null)
            {
                return NotFound();
            }

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Remove")]
        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);
           
            if (providerViewModel == null)
            {
                return NotFound();
            }

            await _providerService.Remove(id);

            if (!ValidOperation())
                return View(providerViewModel);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if(provider == null)
            {
                return NotFound();
            }

            return PartialView("_DetailsAddress", provider);
        }

        [ClaimsAuthorize("Provider", "Update")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if (provider == null)
                return NotFound();

            return PartialView("_UpdateAddress", new ProviderViewModel { Address = provider.Address });
        }

        [ClaimsAuthorize("Provider", "Update")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(ProviderViewModel providerViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid)
                return PartialView("_UpdateAddress", providerViewModel);

            await _providerService.UpdateAddress(_mapper.Map<Address>(providerViewModel.Address));

            if (!ValidOperation())
                return PartialView("_UpdateAddress", providerViewModel);

            var url = Url.Action("GetAddress", "Providers", new { id = providerViewModel.Address.ProviderId });

            return Json(new { success = true, url });
        }

        //Private methods
        private async Task<ProviderViewModel> GetProviderAddress(Guid providerId)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderAddress(providerId));
        }

        private async Task<ProviderViewModel> GetProviderProductAddress(Guid providerId)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductsAddress(providerId));
        }
    }
}
