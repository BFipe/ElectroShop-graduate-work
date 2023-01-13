using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.BusinessLayer.Dtos.AdminDtos
{
    public class TechnoShopUserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public bool IsEmailComfirmed { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}
