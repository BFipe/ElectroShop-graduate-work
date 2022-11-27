using TechnoShop.BusinessLayer.Dtos.ProductDto;
using AutoMapper;
using TechnoShop.Models;
using TechnoShop.BusinessLayer.Dtos.ProductTypeDto;

namespace TechnoShop.MapProfiles
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductViewModel, ProductRequestDto>();
            CreateMap<ProductTypeViewModel, ProductTypeRequestDto>();
        }
    }
}
