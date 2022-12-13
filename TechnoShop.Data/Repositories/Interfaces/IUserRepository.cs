using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<TechnoShopUser> FindUserByEmail(string email);
    }
}
