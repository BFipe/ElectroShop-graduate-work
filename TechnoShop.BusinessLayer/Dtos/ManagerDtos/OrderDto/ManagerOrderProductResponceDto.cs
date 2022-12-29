using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
using TechnoShop.Enums;

namespace TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto
{
    public class ManagerOrderProductResponceDto
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string ProductTypeName { get; set; }
        public int ProductCount { get; set; }
    }
}
