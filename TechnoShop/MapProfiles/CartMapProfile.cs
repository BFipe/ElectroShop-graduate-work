using AutoMapper;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.Models;

namespace TechnoShop.MapProfiles
{
    public class CartMapProfile : Profile
    {
        public CartMapProfile()
        {
            CreateMap<CartResponceDto, CartViewModel>();
        }
    }
}
