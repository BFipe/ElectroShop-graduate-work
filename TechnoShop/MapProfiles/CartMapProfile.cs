using AutoMapper;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.BusinessLayer.Dtos.OrderDto;
using TechnoShop.Models;

namespace TechnoShop.MapProfiles
{
    public class CartMapProfile : Profile
    {
        public CartMapProfile()
        {
            CreateMap<CartResponceDto, CartViewModel>();
            CreateMap<OrderResponceDto, OrderResponceViewModel>();
            CreateMap<OrderProductResponceDto, OrderProductResponceViewModel>();
        }
    }
}
