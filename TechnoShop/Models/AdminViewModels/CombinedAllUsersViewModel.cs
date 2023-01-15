using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Models.AdminViewModels
{
    public class CombinedAllUsersViewModel
    {
        public ResponceStatusViewModel Responce { get; set; } = new();

        public List<string> Roles { get; set; } = new();

        public List<TechnoShopUserViewModel> TechnoShopUsers { get; set; } = new();
    }
}
