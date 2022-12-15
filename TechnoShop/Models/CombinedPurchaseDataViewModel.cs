namespace TechnoShop.Models
{
    public class CombinedPurchaseDataViewModel
    {
        public List<CartViewModel> CartItems { get; set; } = new();

        public UserPurchaseDataViewModel UserPurchaseData { get; set; } = new();
    }
}
