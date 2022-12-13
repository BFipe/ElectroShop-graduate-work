using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.CartDto;

namespace TechnoShop.BusinessLayer.Interfaces
{
    public interface ICartService
    {
        public Task AddToCart(string productId, int cartCount, string userEmail);
        public Task<List<CartResponceDto>> GetProductsFromCart(string userEmail);
        public Task DeleteProductFromCart(string userEmail, string productId);
        public Task ClearCart(string userEmail);
    }
}
