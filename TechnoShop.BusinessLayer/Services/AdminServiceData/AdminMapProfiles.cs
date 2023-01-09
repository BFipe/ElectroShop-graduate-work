using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.AdminDtos;
using TechnoShop.BusinessLayer.Dtos.CartDto;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.BusinessLayer.Services.AdminServiceData
{
    public class AdminMapProfiles : Profile
    {
        public AdminMapProfiles()
        {
            CreateMap<TechnoShopUser, TechnoShopUserDto>();
        }
    }
}
