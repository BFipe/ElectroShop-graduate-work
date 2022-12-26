using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.BusinessLayer.Dtos.OrderDto;

namespace TechnoShop.BusinessLayer.Interfaces
{
    public interface ICartService
    {
        public Task AddToCart(string productId, int cartCount, string userEmail);
        public Task<List<CartResponceDto>> GetProductsFromCart(string userEmail);
        public Task DeleteProductFromCart(string userEmail, string productId);
        public Task ChangeProductQuantity(string userEmail, string productId, int productQuantity);
        public Task ClearCart(string userEmail);
        public Task CreatePurchase(PurchaseUserOrderDataRequestDto purchaseUserOrder,string userEmail);
        public Task CancelOrder(string userEmail, string cancelComment, string orderId);
        public Task<List<OrderResponceDto>> GetUserOrders(string userEmail);
    }
}
