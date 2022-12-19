using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.BusinessLayer.Dtos.CartDto
{
    public class CartResponceDto
    {
        public string Name { get; set; }

        public string ProductTypeName { get; set; }

        public double Cost { get; set; }

        public int CartCount { get; set; }

        public int CartMaxCount { get; set; }

        public string Id { get; set; }

        public bool IsAvaliableForCart { get; set; }
    }
}
