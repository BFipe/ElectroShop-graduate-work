using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.UserOrderEntity;

namespace TechnoShop.Data.Repositories.Interfaces
{
    public interface IManagerRepository
    {
        public Task<List<UserOrder>> GetOrders();
        public Task<UserOrder> OrderById(string orderId);
        public Task SaveAsync();
    }
}
