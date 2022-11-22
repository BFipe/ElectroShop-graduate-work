using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TechnoShop.BusinessLayer.Dtos.Product;
using TechnoShop.Entities.Product;

namespace TechnoShop.BusinessLayer.Services.ProductServiceData
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductRequestDto, Entities.Product.Product>();
            CreateMap<Entities.Product.Product, ProductResponceDTo>();
        }
    }
}
