using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TechnoShop.BusinessLayer.Dtos.CartDto
{
    public class PurchaseUserOrderDataRequestDto
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string FlatNumber { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string OrderComment { get; set; }

        public bool SendEmail { get; set; }
    }
}
