using Microsoft.AspNetCore.Mvc;
using LibraryStore.App.ViewModels;
using LibraryStore.Business.Interfaces;
using AutoMapper;
using LibraryStore.Models;
using LibraryStore.App.Helpers;
using Microsoft.AspNetCore.Authorization;
using LibraryStore.App.Extensions;

namespace LibraryStore.App.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, 
            IProviderRepository providerRepository, 
            IProductService productService, 
            IMapper mapper, 
            INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
            _providerRepository = providerRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders()));
        }

        [AllowAnonymous]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);
           
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Add")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await FillProviders(new ProductViewModel());
            
            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Add")]
        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await FillProviders(productViewModel);

            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }

            var imgPrefix = Guid.NewGuid() + "_";

            if(!await ImageHelper.UploadImage(productViewModel.ImageUpload, imgPrefix, ModelState))
            {
                return View(productViewModel);
            }

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            await _productService.Add(_mapper.Map<Product>(productViewModel));

            if (!ValidOperation())
                return View(productViewModel);

            return RedirectToAction("Index");            
        }

        [ClaimsAuthorize("Product", "Update")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Update")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return NotFound();

            var productAtualization = await GetProduct(id);
            productViewModel.Provider = productAtualization.Provider;
            productViewModel.Image = productAtualization.Image;

            if (!ModelState.IsValid)
            {
                return View(productViewModel);
            }

            if(productViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";
                if(!await ImageHelper.UploadImage(productViewModel.ImageUpload, imgPrefix, ModelState))
                {
                    return View(productViewModel);
                }

                productAtualization.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            }

            productAtualization.Name = productViewModel.Name;
            productAtualization.Description = productViewModel.Description;
            productAtualization.Price = productViewModel.Price;
            productAtualization.Active = productViewModel.Active;

            await _productService.Update(_mapper.Map<Product>(productAtualization));

            if (!ValidOperation())
                return View(productViewModel);

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Product", "Remove")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [ClaimsAuthorize("Product", "Remove")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
                return NotFound();

            await _productService.Remove(id);

            if (!ValidOperation())
                return View(product);

            TempData["Success"] = "Produto excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        //Private methods
        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductProvider(id));
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());
            
            return product;
        }

        private async Task<ProductViewModel> FillProviders(ProductViewModel product)
        {
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());

            return product;
        }
    }
}
