using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.UserOrderEntity;
using TechnoShop.Exceptions;

namespace TechnoShop.BusinessLayer.Services.CartServiceData
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IMapper mapper, IUserRepository userRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public async Task AddToCart(string productId, int cartCount, string userEmail)
        {
            if (cartCount < 0) throw new IncorrectValueException<int>(cartCount);
            var user = await _userRepository.FindUserByEmail(userEmail);
            var product = await _productRepository.GetById(productId);

            if (user == null || product == null) return;
            if (user.Products.Contains(product)) throw new AlreadyInTheCartException();

            _cartRepository.AddProductToCart(user, product, cartCount);

            await _productRepository.Save();
        }

        public async Task<List<CartResponceDto>> GetProductsFromCart(string userEmail)
        {
            List<CartResponceDto> cartResponceDtos = new();

            var user = await _userRepository.FindUserByEmail(userEmail);
            if (user == null) throw new NotFoundException<string>(userEmail);

            cartResponceDtos = _productRepository.GetAll()
                .Where(q => q.TechnoShopUsers.Contains(user))
                .Select(q => new CartResponceDto
                {
                    ProductTypeName = q.ProductTypeName,
                    Cost = q.Cost,
                    Id = q.ProductId,
                    Name = q.Name,
                    IsAvaliableForCart = q.UserCarts.Single(r => r.ProductId == q.ProductId && r.TechnoShopUserId == user.Id).ProductCount <= q.Count - q.InOrderCount,
                    CartCount = q.UserCarts.Single(r => r.ProductId == q.ProductId && r.TechnoShopUserId == user.Id).ProductCount <= q.Count - q.InOrderCount ? q.UserCarts.Single(r => r.ProductId == q.ProductId && r.TechnoShopUserId == user.Id).ProductCount : q.Count - q.InOrderCount,
                    CartMaxCount = q.Count - q.InOrderCount,
                })
                .ToList();

            return cartResponceDtos;
        }

        public async Task DeleteProductFromCart(string userEmail, string productId)
        {
            var user = await _userRepository.FindUserByEmail(userEmail);
            var product = await _productRepository.GetById(productId);
            if (user == null || product == null) return;

            user.Products.Remove(product);
            await _productRepository.Save();
        }

        public async Task ClearCart(string userEmail)
        {
            var user = await _userRepository.FindUserByEmail(userEmail);
            if (user == null) return;

            user.Products.Clear();
            await _productRepository.Save();
        }

        public async Task ChangeProductQuantity(string userEmail, string productId, int productQuantity)
        {
            var user = await _userRepository.FindUserByEmail(userEmail);
            var product = await _productRepository.GetById(productId);
            if (user == null || product == null) return;

            user.UserCarts.Single(q => q.ProductId == product.ProductId).ProductCount = productQuantity;
            await _productRepository.Save();
        }

        public async Task CreatePurchase(PurchaseUserOrderDataRequestDto purchaseUserOrder, string userEmail)
        {
            var user = await _userRepository.FindUserByEmail(userEmail);
            DateTime dateCreated = DateTime.Now;
            UserOrder userOrder = new UserOrder()
            {
                UserOrderId = Guid.NewGuid().ToString(),
                FullName = purchaseUserOrder.FullName,
                PhoneNumber = purchaseUserOrder.PhoneNumber,
                City = purchaseUserOrder.City,
                Street = purchaseUserOrder.Street,
                HouseNumber = purchaseUserOrder.HouseNumber,
                FlatNumber = purchaseUserOrder.FlatNumber,
                Floor = purchaseUserOrder.Floor,
                Entrance = purchaseUserOrder.Entrance,
                OrderComment = purchaseUserOrder.OrderComment,
                
                OrderStatus = OrderStatusEnum.Processing_State,
                DateCreated = dateCreated,
                OrderStatusComment = $"Создан пользователем {user.Email} в {dateCreated.ToLongTimeString}",
                TechnoShopUser = user  
            };

            user.UserCarts.ForEach(q =>
            {
                userOrder.UserOrderProducts.Add(new UserOrderProduct() { Product = q.Product, ProductCount = q.ProductCount });
                q.Product.InOrderCount = q.ProductCount;
                
            });
            
            await _cartRepository.AddNewOrder(userOrder);

            await _productRepository.Save();
            await ClearCart(userEmail);
        }
    }
}
