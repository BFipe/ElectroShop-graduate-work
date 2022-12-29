using AutoMapper;
using TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto;
using TechnoShop.Models.ManagerViewModels;

namespace TechnoShop.MapProfiles
{
    public class ManagerMapProfile : Profile
    {
        public ManagerMapProfile()
        {
            CreateMap<ManagerOrderProductResponceDto, ManagerOrderProductResponceViewModel>();
            CreateMap<ManagerOrderResponceDto, ManagerOrderResponceViewModel>();
        }
    }
}
