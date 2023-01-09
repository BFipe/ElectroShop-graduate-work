using Microsoft.AspNetCore.Identity;

namespace TechnoShop.Models.AdminViewModels
{
    public class CombinedAllRolesViewModel
    {
        public ResponceStatusViewModel Responce { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
