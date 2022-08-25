using Microsoft.AspNetCore.Mvc;
using LibraryStore.App.ViewModels;
using LibraryStore.Business.Interfaces;
using AutoMapper;
using LibraryStore.Models;
using LibraryStore.App.Helpers;

namespace LibraryStore.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IProviderRepository providerRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders()));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);
           
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await FillProviders(new ProductViewModel());
            
            return View(productViewModel);
        }

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
            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

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

            await _productRepository.Edit(_mapper.Map<Product>(productAtualization));

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null)
                return NotFound();

            await _productRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

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
