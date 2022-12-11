using AutoMapper;
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
        public async Task<IActionResult> AddProduct(ProductViewModel productViewModel)
        {
            await GetProductTypeToViewData();
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<ProductRequestDto>(productViewModel);
                    await _productService.AddNewProduct(product);
                    productViewModel.StatusListInfo.Add($"Успешно добавлен новый продукт {product.Name}!");
                }
                catch (Exception ex)
                {
                    productViewModel.ErrorListInfo.Add(ex.Message);
                }
            }
            return View(productViewModel);
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
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
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
                    Id = product.ProductId
                });
            }
            return View(productViewModels.OrderByDescending(q => q.Count).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddInCart(Guid productId)
        {
            Console.WriteLine(productId);
            return RedirectToAction("AllProducts");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            Console.WriteLine(productId);
            return RedirectToAction("AllProducts");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
