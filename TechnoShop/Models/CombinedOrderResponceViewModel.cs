namespace TechnoShop.Models
{
    public class CombinedOrderResponceViewModel
    {
        public List<OrderResponceViewModel> Orders { get; set; } = new();
        public ResponceStatusViewModel ResponceStatus { get; set; } = new();
    }
}
