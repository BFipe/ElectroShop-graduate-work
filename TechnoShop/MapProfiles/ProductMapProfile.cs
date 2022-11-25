using TechnoShop.BusinessLayer.Dtos.ProductDto;
using AutoMapper;
using TechnoShop.Models;

namespace TechnoShop.MapProfiles
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductViewModel, ProductRequestDto>();
        }
    }
}
