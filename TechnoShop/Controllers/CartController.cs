using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.BusinessLayer.Services.ProductServiceData;
using TechnoShop.Models;

namespace TechnoShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CartController(IMapper mapper, ICartService cartService, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _cartService= cartService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> MyCart()
        {
            List<CartViewModel> cart = new ();
            cart = _mapper.Map<List<CartViewModel>>(await _cartService.GetProductsFromCart(_contextAccessor.HttpContext.User.Identity.Name));
            return View(cart);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, string returnUrl, int cartCount)
        {
            try
            {
                await _cartService.AddToCart(productId, cartCount, _contextAccessor.HttpContext.User.Identity.Name);
            }
            catch (Exception)
            {
                RedirectToAction("Index", "Home");
            }
            return returnUrl is null ? RedirectToAction("Index", "Home") : Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            try
            {
                await _cartService.DeleteProductFromCart(_contextAccessor.HttpContext.User.Identity.Name, productId);
            }
            catch (Exception)
            {
                Redirect("MyCart");
            }
            return Redirect("MyCart");
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangeQuantity(string productId, int productQuantity)
        {
            if (productQuantity < 0) return Redirect("MyCart");
            try
            {
                await _cartService.ChangeProductQuantity(_contextAccessor.HttpContext.User.Identity.Name, productId, productQuantity);
            }
            catch (Exception)
            {
                return Redirect("MyCart");
            }
            return Redirect("MyCart");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                await _cartService.ClearCart(_contextAccessor.HttpContext.User.Identity.Name);
            }
            catch (Exception)
            {
                return Redirect("MyCart");
            }
            return Redirect("MyCart");
        }
    }
}
