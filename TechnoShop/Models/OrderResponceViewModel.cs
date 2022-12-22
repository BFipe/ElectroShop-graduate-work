using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.Enums;

namespace TechnoShop.Models
{
    public class OrderResponceViewModel
    {
        public string UserOrderId { get; set; }

        public DateTime DateCreated { get; set; }

        public string FullName { get; set; }

        public float PhoneNumber { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string OrderComment { get; set; }

        public OrderStatusEnum? OrderStatus { get; set; }

        public List<OrderProductResponceViewModel> Products { get; set; } = new();
    }
}
