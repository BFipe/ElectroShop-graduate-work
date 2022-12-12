using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
using TechnoShop.BusinessLayer.Dtos.ProductTypeDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Models;


namespace TechnoShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductController(IMapper mapper, IProductService productService, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _productService = productService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        private async Task GetProductTypeToViewData()
        {
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            var productTypes = await _productService.GetProductTypes();
            foreach (var type in productTypes)
            {
                selectListItem.Add(new SelectListItem(type.TypeName, type.TypeName));
            }
            ViewData["ProductTypes"] = selectListItem;
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            await GetProductTypeToViewData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductRequestViewModel productRequestViewModel)
        {
            await GetProductTypeToViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<ProductRequestDto>(productRequestViewModel);
                    await _productService.AddNewProduct(product);
                    productRequestViewModel.StatusListInfo.Add($"Успешно добавлен новый продукт {product.Name}!");
                }
                catch (Exception ex)
                {
                    productRequestViewModel.ErrorListInfo.Add(ex.Message);
                }
            }
            return View(productRequestViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddProductType()
        {
            await GetProductTypeToViewData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductType(ProductTypeViewModel productTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var productType = _mapper.Map<ProductTypeRequestDto>(productTypeViewModel);
                    await _productService.AddNewType(productType);
                    productTypeViewModel.StatusListInfo.Add($"Успешно добавлен новый тип {productType.TypeName}!");
                }
                catch (Exception ex)
                {
                    productTypeViewModel.ErrorListInfo.Add(ex.Message);
                }
            }
            await GetProductTypeToViewData();
            return View(productTypeViewModel);
        }

        public async Task<IActionResult> AllProductTypes()
        {
            List<ProductTypeViewModel> productTypeViewModels = new List<ProductTypeViewModel>();
            var productTypes = await _productService.GetProductTypes();
            foreach (var type in productTypes)
            {
                productTypeViewModels.Add(new ProductTypeViewModel()
                {
                    TypeName = type.TypeName,
                });
            }
            await GetProductTypeToViewData();
            return View(productTypeViewModels);
        }

        public async Task<IActionResult> AllProducts()
        {
            ViewData["returnUrl"] = Request.GetDisplayUrl();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            try
            {
                var products = await _productService.GetProducts();
                foreach (var product in products)
                {
                    productViewModels.Add(new ProductViewModel()
                    {
                        Cost = product.Cost,
                        Name = product.Name,
                        Description = product.Description,
                        Count = product.Count,
                        ProductTypeName = product.ProductTypeName,
                        Id = product.ProductId,
                        ProductRate = product.ProductRate
                    });
                }
            }
            catch (Exception)
            {
                RedirectToAction("Index", "Home");
            }
            return View(productViewModels);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            if (productId == null) RedirectToAction("AllProducts", "Product");
            ErrorViewModel errorViewModel = new();
            try
            {
                await _productService.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                errorViewModel.ErrorMessage = ex.Message;
            }
            return RedirectToAction("AllProducts", errorViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AddToCart(string productId, string returnUrl)
        {
            try
            {

            }
            catch (Exception)
            {

            }
            return returnUrl is null ? RedirectToAction("Index", "Home") : Redirect(returnUrl);
        }
    }
}
