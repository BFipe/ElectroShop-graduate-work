namespace TechnoShop.Models.AdminViewModels
{
    public class TechnoShopUserViewModel
    {
        public string Email { get; set; }

        public string Id { get; set; }

        public bool IsEmailComfirmed { get; set; }

        public List<string> Roles { get; set; }
    }
}
