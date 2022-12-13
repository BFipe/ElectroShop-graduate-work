using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechnoShop.Models
{
    public class CartViewModel
    {
        public string Name { get; set; }

        public string ProductTypeName { get; set; }

        public double Cost { get; set; }

        public int CartCount { get; set; }

        public string Id { get; set; }

    }
}
