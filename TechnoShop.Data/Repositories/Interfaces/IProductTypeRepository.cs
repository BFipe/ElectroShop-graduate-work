using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.ProductEntity;

namespace TechnoShop.Data.Repositories.Interfaces
{
    public interface IProductTypeRepository : IRepository<ProductType>
    {
        Task<bool> IsExists(string name);
        Task<ProductType> GetByName(string name);
    }
}
