using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.ProductServiceData;
using TechnoShop.Entities.Enums;

namespace TechnoShop.BusinessLayer.Interfaces
{
    public interface IProductService
    {
        public Task AddNewProduct(ProductRequestDto product);
    }
}
