using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
using TechnoShop.BusinessLayer.Dtos.ProductTypeDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Exceptions;
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
            ModelState.Remove("ResponceStatus.ErrorMessage");
            ModelState.Remove("ResponceStatus.SucessMessage");
            await GetProductTypeToViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<ProductRequestDto>(productRequestViewModel);
                    await _productService.AddNewProduct(product);
                    productRequestViewModel.ResponceStatus.SucessMessage = $"Успешно добавлен новый продукт {product.Name}!";
                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    productRequestViewModel.ResponceStatus.ErrorMessage = ex.Message;
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
            ModelState.Remove("ResponceStatus.ErrorMessage");
            ModelState.Remove("ResponceStatus.SucessMessage");
            if (ModelState.IsValid)
            {
                try
                {
                    var productType = _mapper.Map<ProductTypeRequestDto>(productTypeViewModel);
                    await _productService.AddNewType(productType);
                    productTypeViewModel.ResponceStatus.SucessMessage = $"Успешно добавлен новый тип {productType.TypeName}!";
                }
                catch (Exception ex)
                {
                    productTypeViewModel.ResponceStatus.ErrorMessage = ex.Message;
                }
            }
            ModelState.Clear();
            await GetProductTypeToViewData();
            return View(productTypeViewModel);
        }

        public async Task<IActionResult> AllProducts(string productType, int page = 1, int productsPerPage = 6, ResponceStatusViewModel? responceStatusViewModel = null)
        {
            ViewData["returnUrl"] = Request.GetDisplayUrl();
            CombinedPageProductViewModel combinedPageProductViewModel= new();
            combinedPageProductViewModel.ResponceStatusViewModel = responceStatusViewModel;
            List<ProductResponceViewModel> productViewModels = new();
            try
            {
                var products = await _productService.GetProducts(_contextAccessor.HttpContext.User.Identity.Name, productType, page, productsPerPage);

                foreach (var product in products)
                {
                    productViewModels.Add(new ProductResponceViewModel()
                    {
                        Cost = product.Cost,
                        Name = product.Name,
                        Description = product.Description,
                        Count = product.Count - product.InOrderCount,
                        ProductTypeName = product.ProductTypeName,
                        PictureLink = product.PictureLink,
                        Id = product.ProductId,
                        ProductRate = product.ProductRate,
                        IsOpenForCart = product.IsOpenForCart
                    });
                }

                combinedPageProductViewModel.Products = productViewModels;
                combinedPageProductViewModel.CurrentPage = page;
                combinedPageProductViewModel.ProductsPerPage = productsPerPage;
                combinedPageProductViewModel.ProductType = productType;
                combinedPageProductViewModel.ProductCount = _productService.GetProductCount(productType);
                var productTypes = await _productService.GetProductTypes();
                productTypes.ForEach(type =>
                {
                    combinedPageProductViewModel.AllProductTypes.Add(type.TypeName);
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("AllProducts", "Product");
            }
            return View(combinedPageProductViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            if (productId == null) RedirectToAction("AllProducts", "Product");
            ResponceStatusViewModel responceStatusViewModel = new();
            try
            {
                await _productService.DeleteProduct(productId);
                responceStatusViewModel.SucessMessage = "Продукт был успешно удален";
            }
            catch (Exception ex)
            {
                responceStatusViewModel.ErrorMessage = ex.Message;
            }
            return RedirectToAction("AllProducts", responceStatusViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteProductType(string productTypeName)
        {
            if (String.IsNullOrWhiteSpace(productTypeName)) RedirectToAction("AddProductType", "Product");
            ResponceStatusViewModel responceStatusViewModel = new();
            try
            {
                await _productService.DeleteProductType(productTypeName);
                responceStatusViewModel.SucessMessage = "Тип продукта был успешно удален";
            }
            catch (Exception ex)
            {
                responceStatusViewModel.ErrorMessage = ex.Message;
            }
            return RedirectToAction("AddProductType", responceStatusViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> EditProduct(string productId)
        {
            if (String.IsNullOrEmpty(productId)) return Redirect("AllProducts");

            await GetProductTypeToViewData();
            var product = await _productService.GetProduct(productId);

            if (product is null) return Redirect("AllProducts");

            ProductEditViewModel productResponceViewModel = new()
            {
                Id = product.ProductId,
                ProductRate = product.ProductRate,
                Description = product.Description,
                Cost = product.Cost,
                Count = product.Count,
                Name = product.Name,
                PictureLink = product.PictureLink,
                ProductTypeName = product.ProductTypeName,
                MinCount = product.InOrderCount
            };

            return View(productResponceViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> EditProduct(string productId, ProductEditViewModel productEditViewModel)
        {
            ModelState.Remove("ResponceStatus.ErrorMessage");
            ModelState.Remove("ResponceStatus.SucessMessage");
            ModelState.Remove("Id");
            await GetProductTypeToViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    var productRequest = _mapper.Map<ProductRequestDto>(productEditViewModel);
                    await _productService.UpdateProduct(productId, productRequest);
                    ResponceStatusViewModel responceStatus = new();
                    responceStatus.SucessMessage = $"Успешно обновлен продукт с именем {productEditViewModel.Name}";
                    return RedirectToAction("AllProducts", responceStatus);
                }
                catch (Exception ex)
                {
                    productEditViewModel.ResponceStatus.ErrorMessage = ex.Message;
                } 
            }
            return View(productEditViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
