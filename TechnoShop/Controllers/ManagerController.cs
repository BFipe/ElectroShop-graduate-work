using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Models.ManagerViewModels;

namespace TechnoShop.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IManagerService _managerService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ManagerController(IMapper mapper, IManagerService managerService, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _managerService = managerService;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders()
        {
            CombinedManagerOrderResponceViewModel combinedManagerOrderResponce = new();
            combinedManagerOrderResponce.Orders =_mapper.Map<List<ManagerOrderResponceViewModel>>(await _managerService.GetUserOrders());
           
            return View(combinedManagerOrderResponce);
        }
    }
}
