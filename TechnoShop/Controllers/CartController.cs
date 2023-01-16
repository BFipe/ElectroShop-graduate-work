using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using TechnoShop.BusinessLayer.Dtos.CartDto;
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
            _cartService = cartService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> MyCart()
        {
            List<CartViewModel> cart = new();
            cart = _mapper.Map<List<CartViewModel>>(await _cartService.GetProductsFromCart(_contextAccessor.HttpContext.User.Identity.Name)).ToList();
            return View(cart);
        }

        [HttpGet]
        public IActionResult AddToCart()
        {
            return RedirectToAction("Index", "Home");
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
                return Redirect("MyCart");
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

        [HttpGet]
        public async Task<IActionResult> PurchaseConfirmation()
        {
            CombinedPurchaseDataViewModel purchaseData = new();

            List<CartViewModel> cart = _mapper.Map<List<CartViewModel>>(await _cartService.GetProductsFromCart(_contextAccessor.HttpContext.User.Identity.Name)).Where(q => q.IsAvaliableForCart).ToList();
            if (cart.Any() == false) return Redirect("MyCart");

            purchaseData.CartItems = cart;
            return View(purchaseData);
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseConfirmation([FromForm] UserOrderDataViewModel userPurchaseDataViewModel)
        {
            if (userPurchaseDataViewModel == null)
            {
                return Redirect("PurchaseConfirmation");
            }

            CombinedPurchaseDataViewModel purchaseData = new();

            purchaseData.UserPurchaseData = userPurchaseDataViewModel;

            List<CartViewModel> cart = _mapper.Map<List<CartViewModel>>(await _cartService.GetProductsFromCart(_contextAccessor.HttpContext.User.Identity.Name));

            if (cart.Any() == false) return Redirect("MyCart");

            purchaseData.CartItems = cart;

            if (
                !String.IsNullOrWhiteSpace(userPurchaseDataViewModel.FullName) &&
                !String.IsNullOrWhiteSpace(userPurchaseDataViewModel.PhoneNumber) &&
                !String.IsNullOrWhiteSpace(userPurchaseDataViewModel.City) &&
                !String.IsNullOrWhiteSpace(userPurchaseDataViewModel.Street) &&
                !String.IsNullOrWhiteSpace(userPurchaseDataViewModel.HouseNumber)
               )
            {
                string validPhone = DecharisePhoneNumber(userPurchaseDataViewModel.PhoneNumber);

                if (validPhone == String.Empty)
                {
                    purchaseData.UserPurchaseData.PhoneNumber = String.Empty;
                }
                else
                {
                    PurchaseUserOrderDataRequestDto purchaseUserOrder = new PurchaseUserOrderDataRequestDto()
                    {
                        FullName = userPurchaseDataViewModel.FullName,
                        City = userPurchaseDataViewModel.City,
                        Entrance = userPurchaseDataViewModel.Entrance,
                        FlatNumber = userPurchaseDataViewModel.FlatNumber,
                        Floor = userPurchaseDataViewModel.Floor,
                        HouseNumber = userPurchaseDataViewModel.HouseNumber,
                        OrderComment = userPurchaseDataViewModel.OrderComment,
                        Street = userPurchaseDataViewModel.Street,
                        PhoneNumber = validPhone,
                        SendEmail = userPurchaseDataViewModel.SendEmail,
                    };

                    await _cartService.CreatePurchase(purchaseUserOrder, _contextAccessor.HttpContext.User.Identity.Name);
                    
                    return Redirect("MyOrders");
                }
            }
            return View(purchaseData);
        }

        private string DecharisePhoneNumber(string phoneNumber)
        {
            string[] phoneFilterChar = { " ", "+", "(", ")", "-" };
            foreach (var pchar in phoneFilterChar)
            {
                phoneNumber = phoneNumber.Replace(pchar, "");
            }
            bool result = float.TryParse(phoneNumber, out float number) && (phoneNumber.Count() == 11 || phoneNumber.Count() == 12);
            if (result == false) return String.Empty;

            var charArray = phoneNumber.ToArray();
            if (charArray[0] == '3')
            {
                phoneNumber = $"+{charArray[0]}{charArray[1]}{charArray[2]} ({charArray[3]}{charArray[4]}) {charArray[5]}{charArray[6]}{charArray[7]}-{charArray[8]}{charArray[9]}-{charArray[10]}{charArray[11]}";
                Console.WriteLine(phoneNumber);
            }
            if (charArray[0] == '8')
            {
                phoneNumber = $"{charArray[0]} ({charArray[1]}{charArray[2]}{charArray[3]}) {charArray[4]}{charArray[5]}{charArray[6]}-{charArray[7]}{charArray[8]}-{charArray[9]}{charArray[10]}";
                Console.WriteLine(phoneNumber);
            }
            return phoneNumber;
        }

        public async Task<IActionResult> MyOrders()
        {
            CombinedOrderResponceViewModel combinedOrderResponceViewModel = new();
            var orderResponce = _mapper.Map<List<OrderResponceViewModel>>(await _cartService.GetUserOrders(_contextAccessor.HttpContext.User.Identity.Name)).OrderByDescending(q => q.DateCreated).ToList();
            combinedOrderResponceViewModel.Orders = orderResponce;
            return View(combinedOrderResponceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(string cancelComment, string orderId, string cancelationReason)
        {
            if (String.IsNullOrWhiteSpace(orderId)) Redirect("MyOrders");

            try
            {
                string cancelRequestComment = $"Отменен пользователем по причине: \"{cancelationReason}\" с комментарием: \"{cancelComment}\" в {DateTime.Now.ToString()}";
                await _cartService.CancelOrder(_contextAccessor.HttpContext.User.Identity.Name, cancelRequestComment, orderId);
            }
            catch (Exception ex)
            {
                ViewData["ExceptionMessage"] = ex.Message;
            }
            return Redirect("MyOrders");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(string orderId)
        {
            if (String.IsNullOrWhiteSpace(orderId)) Redirect("MyOrders");
            try
            { 
                await _cartService.ConfirmOrder(_contextAccessor.HttpContext.User.Identity.Name, orderId);
            }
            catch (Exception ex)
            {
                ViewData["ExceptionMessage"] = ex.Message;
            }
            return Redirect("MyOrders");
        }
    }
}

