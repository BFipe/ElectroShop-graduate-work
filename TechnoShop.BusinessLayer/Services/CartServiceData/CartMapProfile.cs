using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.BusinessLayer.Dtos.ProductDto;
using TechnoShop.BusinessLayer.Dtos.ProductTypeDto;
using TechnoShop.Entities.ProductEntity;

namespace TechnoShop.BusinessLayer.Services.ProductServiceData
{
    public class CartMapProfile : Profile
    {
        public CartMapProfile()
        {
            CreateMap<Product, CartResponceDto>();
        }
    }
}
