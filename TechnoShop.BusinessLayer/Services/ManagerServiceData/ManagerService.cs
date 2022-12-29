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
    }
}
