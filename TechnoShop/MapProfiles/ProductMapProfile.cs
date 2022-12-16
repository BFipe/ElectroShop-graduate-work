using TechnoShop.BusinessLayer.Dtos.ProductDto;
using AutoMapper;
using TechnoShop.Models;
using TechnoShop.BusinessLayer.Dtos.ProductTypeDto;
using TechnoShop.BusinessLayer.Dtos.CartDto;

namespace TechnoShop.MapProfiles
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductResponceViewModel, ProductRequestDto>();
            CreateMap<ProductRequestViewModel, ProductRequestDto>();

            CreateMap<ProductTypeViewModel, ProductTypeRequestDto>();
        }
    }
}
