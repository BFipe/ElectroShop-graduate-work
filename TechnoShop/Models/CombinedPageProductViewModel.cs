namespace TechnoShop.Models
{
    public class CombinedPageProductViewModel
    {
        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int ProductCount { get; set; }

        public string ProductType { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
