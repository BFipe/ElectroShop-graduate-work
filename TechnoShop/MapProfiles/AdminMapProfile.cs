using AutoMapper;
using TechnoShop.BusinessLayer.Dtos.AdminDtos;
using TechnoShop.Models.AdminViewModels;

namespace TechnoShop.MapProfiles
{
    public class AdminMapProfile : Profile
    {
        public AdminMapProfile()
        {
            CreateMap<TechnoShopUserDto, TechnoShopUserViewModel>();
        }
    }
}
