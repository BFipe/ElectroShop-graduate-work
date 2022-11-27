using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
using TechnoShop.BusinessLayer.Dtos.ProductTypeDto;
using TechnoShop.Entities.ProductEntity;

namespace TechnoShop.BusinessLayer.Services.ProductServiceData
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductRequestDto, Product>();
            CreateMap<Product, ProductRequestDto>();
            CreateMap<Product, ProductResponceDto>();

            CreateMap<ProductTypeRequestDto, ProductType>();
            CreateMap<ProductType, ProductTypeRequestDto>();
            CreateMap<ProductType, ProductTypeResponceDto>();
        }
    }
}
