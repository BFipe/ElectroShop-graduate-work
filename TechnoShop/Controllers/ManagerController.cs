using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TechnoShop.BusinessLayer.Dtos.ManagerDtos.OrderDto;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.BusinessLayer.Services.CartServiceData;
using TechnoShop.Models.ManagerViewModels;

namespace TechnoShop.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
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

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Orders()
        {
            CombinedManagerOrderResponceViewModel combinedManagerOrderResponce = new();
            combinedManagerOrderResponce.Orders = _mapper.Map<List<ManagerOrderResponceViewModel>>(await _managerService.GetUserOrders());

            return View(combinedManagerOrderResponce);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ConfirmOrder(string orderId)
        {
            if (String.IsNullOrWhiteSpace(orderId)) return Redirect("Orders");

            try
            {
                await _managerService.ConfirmOrder(orderId);
            }
            catch (Exception ex)
            {
                ViewData["ExceptionMessage"] = ex.Message;

            }
            return Redirect("Orders");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CancelOrder(string orderId, string cancelComment, string cancelationReason)
        {
            if (String.IsNullOrWhiteSpace(orderId)) Redirect("Orders");

            try
            {
                string cancelRequestComment = $"Отменен менеджером по причине: \"{cancelationReason}\" с комментарием: \"{cancelComment}\" в {DateTime.Now.ToString()}";
                await _managerService.CancelOrder(cancelRequestComment, orderId);
            }
            catch (Exception ex)
            {
                ViewData["ExceptionMessage"] = ex.Message;

            }
            return Redirect("Orders");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            if (String.IsNullOrWhiteSpace(orderId)) Redirect("Orders");

            try
            {
                await _managerService.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                ViewData["ExceptionMessage"] = ex.Message;
            }
            return Redirect("Orders");
        }
    }
}
