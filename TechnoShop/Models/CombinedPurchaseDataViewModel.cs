namespace TechnoShop.Models
{
    public class CombinedPurchaseDataViewModel
    {
        public List<CartViewModel> CartItems { get; set; } = new();

        public UserOrderDataViewModel UserPurchaseData { get; set; } = new();
    }
}
