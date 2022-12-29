using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto;
using TechnoShop.Entities.UserOrderEntity;

namespace TechnoShop.BusinessLayer.Services.ManagerServiceData
{
    public class ManagerMapProfile : Profile
    {
        public ManagerMapProfile()
        {
            CreateMap<UserOrder, ManagerOrderResponceDto>();
            CreateMap<UserOrderProduct, ManagerOrderProductResponceDto>();
        }
    }
}
