using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto;
using TechnoShop.BusinessLayer.Dtos.OrderDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Exceptions;

namespace TechnoShop.BusinessLayer.Services.ManagerServiceData
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public ManagerService(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository= managerRepository;
            _mapper= mapper;
        }

        public async Task<List<ManagerOrderResponceDto>> GetUserOrders()
        {
            List<ManagerOrderResponceDto> managerOrderResponces = new();
            var x = await _managerRepository.GetOrders();
            foreach (var order in x)
            {
                ManagerOrderResponceDto managerOrderResponce = new()
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
                    Street= order.Street,

                    UserEmail = order.TechnoShopUser.Email
                };  

                foreach (var orderProduct in order.UserOrderProducts)
                {
                    ManagerOrderProductResponceDto managerOrderProductResponce = new()
                    {
                        ProductId = orderProduct.ProductId,
                        Name = orderProduct.Product.Name,
                        Cost = orderProduct.Product.Cost,
                        ProductTypeName = orderProduct.Product.ProductTypeName,
                        ProductCount = orderProduct.ProductCount
                    };
                    managerOrderResponce.Products.Add(managerOrderProductResponce);
                }
                
                managerOrderResponces.Add(managerOrderResponce);
            }

            return managerOrderResponces.OrderBy(q => q.OrderStatus).ThenByDescending(q => q.DateCreated).ToList();
        }

        public async Task CancelOrder(string cancelRequestComment, string orderId)
        {
            var order = await _managerRepository.OrderById(orderId);
            if (order == null) throw new NotFoundException<string>(orderId);

            order.OrderStatus = Enums.OrderStatusEnum.Canceled_By_Manager;
            order.OrderStatusComment = cancelRequestComment;

            order.UserOrderProducts.ForEach(q =>
            {
                q.Product.InOrderCount -= q.ProductCount;
            });

            await _managerRepository.SaveAsync();
        }

        public async Task ConfirmOrder(string orderId)
        {
            var order = await _managerRepository.OrderById(orderId);
            if (order == null) throw new NotFoundException<string>(orderId);

            order.OrderStatus = Enums.OrderStatusEnum.Confirmed;
            order.OrderStatusComment = $"Подтвержден менеджером {DateTime.Now.ToString()}";

            await _managerRepository.SaveAsync();
        }

        public async Task DeleteOrder(string orderId)
        {
            var order = await _managerRepository.OrderById(orderId);
            if (order == null) throw new NotFoundException<string>(orderId);

            if (order.OrderStatus != Enums.OrderStatusEnum.Canceled_By_Manager && order.OrderStatus != Enums.OrderStatusEnum.Canceled_By_User && order.OrderStatus != Enums.OrderStatusEnum.Finished_Sucessfully)
            {
                order.UserOrderProducts.ForEach(q =>
                {
                    q.Product.InOrderCount -= q.ProductCount;
                });
            }

            await _managerRepository.DeleteOrder(orderId);
            await _managerRepository.SaveAsync();
        }
    }
}
