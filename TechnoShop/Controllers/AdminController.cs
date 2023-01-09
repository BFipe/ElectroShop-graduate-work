using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Entities.UserEntity;
using TechnoShop.Models;
using TechnoShop.Models.AdminViewModels;

namespace TechnoShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;


        public AdminController(ILogger<AdminController> logger, IAdminService adminService, IMapper mapper)
        {
           _adminService= adminService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> AllRoles(ResponceStatusViewModel responceStatus)
        {
            CombinedAllRolesViewModel combinedAllRolesViewModel = new CombinedAllRolesViewModel();
            combinedAllRolesViewModel.Responce = responceStatus;
            try
            {
                combinedAllRolesViewModel.Roles = await _adminService.AllRoles();
            }
            catch (Exception ex)
            {
                combinedAllRolesViewModel.Responce.ErrorMessage += " " + ex.Message;
            }
    
            return View(combinedAllRolesViewModel);
        }

        public async Task<IActionResult> AllUsers(ResponceStatusViewModel responceStatus)
        {
            CombinedAllUsersViewModel combinedAllUsersViewModel = new CombinedAllUsersViewModel();
            combinedAllUsersViewModel.Responce = responceStatus;
            try
            {
                combinedAllUsersViewModel.TechnoShopUsers = _mapper.Map<List<TechnoShopUserViewModel>>(await _adminService.AllUsers());
            }
            catch (Exception ex)
            {
                combinedAllUsersViewModel.Responce.ErrorMessage = ex.Message;
            }
            return View(combinedAllUsersViewModel);
        }

        [HttpPost]
        public async  Task<IActionResult> AddRole(string roleName)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if(String.IsNullOrEmpty(roleName) == false)
            {
                IdentityResult result = await _adminService.AddRole(roleName);
                if (result.Succeeded == false)
                {
                    responceStatusViewModel.ErrorMessage = result.Errors.FirstOrDefault().Description;
                }
                else
                {
                    responceStatusViewModel.SucessMessage = $"Роль {roleName} успешно добавлена!";
                }
            }
            return RedirectToAction("AllRoles", responceStatusViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(roleId) == false)
            {
                IdentityResult result = await _adminService.DeleteRole(roleId);
                if (result.Succeeded == false)
                {
                    responceStatusViewModel.ErrorMessage = result.Errors.FirstOrDefault().ToString();
                }
                else
                {
                    responceStatusViewModel.SucessMessage = "Роль успешно удалена!";
                }
            }
            return RedirectToAction("AllRoles", responceStatusViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}