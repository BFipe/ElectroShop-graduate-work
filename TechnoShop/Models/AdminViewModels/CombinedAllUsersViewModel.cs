using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Models.AdminViewModels
{
    public class CombinedAllUsersViewModel
    {
            public ResponceStatusViewModel Responce { get; set; }

            public List<TechnoShopUserViewModel> TechnoShopUsers { get; set; }
    }
}
