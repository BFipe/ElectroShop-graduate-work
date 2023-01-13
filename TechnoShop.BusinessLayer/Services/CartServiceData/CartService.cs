using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.BusinessLayer.Dtos.OrderDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.UserOrderEntity;
using TechnoShop.Enums;
using TechnoShop.Exceptions;

namespace TechnoShop.BusinessLayer.Services.CartServiceData
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IEmailSenderService _emailSender;
        private readonly IMapper _mapper;

        public CartService(IProductRepository productRepository, IProductTypeRepository productTypeRepository, IMapper mapper, IUserRepository userRepository, ICartRepository cartRepository, IEmailSenderService emailSender)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _emailSender = emailSender;
        }

        public async Task AddToCart(string productId, int cartCount, string userEmail)
        {
            if (cartCount < 0) throw new IncorrectValueException<int>(cartCount);
            var user = await _userRepository.FindUserByEmail(userEmail);
            var product = await _productRepository.GetById(productId);

            if (user == null || product == null) return;
            if (product.Count - product.InOrderCount < cartCount) return;
            if (user.Products.Contains(product)) throw new AlreadyInTheCartException();

            _cartRepository.AddProductToCart(user, product, cartCount);

            await _productRepository.Save();
        }

        public async Task<List<CartResponceDto>> GetProductsFromCart(string userEmail)
        {
            List<CartResponceDto> cartResponceDtos = new();

            var user = await _userRepository.FindUserByEmail(userEmail);
            if (user == null) throw new NotFoundException<string>(userEmail);

            cartResponceDtos = user.Products
                .Select(q => new CartResponceDto
                {

                    ProductTypeName = q.ProductTypeName,
                    Cost = q.Cost,
                    Id = q.ProductId,
                    Name = q.Name,
                    IsAvaliableForCart = q.Count - q.InOrderCount > 0,
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
            var dateCreated = DateTime.Now.ToString();
            var orderNumber = await _cartRepository.OrdersCount() + 1;
            UserOrder userOrder = new UserOrder()
            {
                UserOrderId = Guid.NewGuid().ToString(),

                OrderNumber = orderNumber.ToString(),

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
                OrderStatusComment = $"Создан пользователем {user.Email} в {dateCreated}",
                TechnoShopUser = user
            };

            user.UserCarts
                .Where(q => q.Product.Count - q.Product.InOrderCount > 0)
                .ToList()
                .ForEach(q =>
            {
                userOrder.UserOrderProducts.Add(new UserOrderProduct() { Product = q.Product, ProductCount = q.ProductCount });
                q.Product.InOrderCount += q.ProductCount;
            });

            await _cartRepository.AddNewOrder(userOrder);
            await _productRepository.Save();

            if (purchaseUserOrder.SendEmail)
            {
               var htmlMessage = $@"
<h1>Спасибо за оформление заказа!</h1>
<p>Номер вашего заказа - {orderNumber}.</p>

<p>В ближайшее время с вами свяжется менеджер для его подтверждения!</p>

<p>Список всех ваших заказов - https://localhost:7002/Cart/MyOrders</p>
";
               await _emailSender.SendEmailAsync(user.Email, $"TechnoShop, оформление заказа №{orderNumber}", htmlMessage);
                
            }

            await ClearCart(userEmail);
        }

        public async Task<List<OrderResponceDto>> GetUserOrders(string userEmail)
        {
            List<OrderResponceDto> orderResponceDtos = new();

            var user = await _userRepository.FindUserByEmail(userEmail);
            if (user == null) throw new NotFoundException<string>(userEmail);

            var userOrders = user.UserOrders.ToList();

            foreach (var order in userOrders)
            {
                OrderResponceDto orderResponceDto = new()
                {
                    UserOrderId = order.UserOrderId,
                    OrderNumber = order.OrderNumber,
                    DateCreated = order.DateCreated,
                    FlatNumber = order.FlatNumber,
                    Floor = order.Floor,
                    FullName = order.FullName,
                    City = order.City,
                    Entrance = order.Entrance,
                    HouseNumber = order.HouseNumber,
                    OrderComment = order.OrderComment,
                    OrderStatus = order.OrderStatus,
                    PhoneNumber = order.PhoneNumber,
                    Street = order.Street,
                };
                foreach (var userOrder in order.UserOrderProducts)
                {
                    OrderProductResponceDto orderProductResponceDto = new()
                    {
                        ProductId = userOrder.Product.ProductId,
                        Name= userOrder.Product.Name,
                        ProductTypeName = userOrder.Product.ProductTypeName,
                        Cost= userOrder.Product.Cost,
                        
                        ProductCount = userOrder.ProductCount,
                    };

                    orderResponceDto.Products.Add(orderProductResponceDto);
                }

                orderResponceDtos.Add(orderResponceDto);
            }

            return orderResponceDtos;
        }

        public async Task CancelOrder(string userEmail, string cancelComment, string orderId)
        {
            var user = await _userRepository.FindUserByEmail(userEmail);
            if (user == null) throw new NotFoundException<string>(userEmail);

            var userOrder = user.UserOrders.FirstOrDefault(q => q.UserOrderId == orderId);

            if (userOrder == null) throw new ObjectNotExistsException();

            userOrder.OrderStatusComment = cancelComment;
            userOrder.OrderStatus = OrderStatusEnum.Canceled_By_User;

            userOrder.UserOrderProducts.ForEach(q =>
            {
                q.Product.InOrderCount -= q.ProductCount;
            });

            await _productRepository.Save();
        }
    }
}
