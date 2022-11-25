using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
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
                    ProductType = product.ProductType,
                });
            }
            return View(productViewModels);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel productViewModel)
        {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
