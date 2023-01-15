using MailKit.Search;
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
        public Task CancelOrder(string cancelRequestComment,string orderId);
        public Task ConfirmOrder(string orderId);
        public Task DeleteOrder(string orderId);
    }
}
