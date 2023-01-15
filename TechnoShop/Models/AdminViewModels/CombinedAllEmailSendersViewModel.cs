using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Models.AdminViewModels
{
    public class CombinedAllEmailSendersViewModel
    {
        public ResponceStatusViewModel Responce { get; set; } = new();

        public List<string> EmailSenders { get; set; }
    }
}
