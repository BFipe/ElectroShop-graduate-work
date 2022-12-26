using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;
using TechnoShop.Enums;

namespace TechnoShop.Entities.UserOrderEntity
{
    public class UserOrder
    {
        public string UserOrderId { get; set; }

        public string OrderNumber { get; set; }

        public string DateCreated { get; set; }

        public string FullName { get; set;}

        public string PhoneNumber { get; set;}

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string OrderComment { get; set; }

        public OrderStatusEnum? OrderStatus { get; set; }

        public DateTime? OrderCompletionDate { get; set; }

        public string OrderStatusComment { get; set; }


        public string TechnoShopUserId { get; set;}
        public TechnoShopUser TechnoShopUser { get; set; }
    
        public List<Product> Products { get; set; } = new();
        
        public List<UserOrderProduct> UserOrderProducts { get; set; } = new(); 
    }
}
