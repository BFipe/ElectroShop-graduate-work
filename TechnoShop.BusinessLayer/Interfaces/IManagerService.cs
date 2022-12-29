using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto;

namespace TechnoShop.BusinessLayer.Interfaces
{
    public interface IManagerService
    {
        public Task<List<ManagerOrderResponceDto>> GetUserOrders();
    }
}
