using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.Enums;

namespace TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto
{
    public class ManagerOrderResponceDto
    {
        public string UserOrderId { get; set; }

        public string UserEmail { get; set; }

        public string OrderNumber { get; set; }

        public string DateCreated { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string OrderComment { get; set; }

        public OrderStatusEnum? OrderStatus { get; set; }

        public List<ManagerOrderProductResponceDto> Products { get; set; } = new();
    }

}
