namespace TechnoShop.Models.ManagerViewModels
{
    public class CombinedManagerOrderResponceViewModel
    {
        public List<ManagerOrderResponceViewModel> Orders { get; set; }
        public ResponceStatusViewModel ResponceStatus { get; set; } = new();
    }
}
