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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSenderService;


        public AdminController(ILogger<AdminController> logger, IAdminService adminService, IMapper mapper, IEmailSenderService emailSenderService)
        {
            _adminService = adminService;
            _logger = logger;
            _mapper = mapper;
            _emailSenderService = emailSenderService;
        }

        public IActionResult Index(ResponceStatusViewModel responceStatusViewModel)
        {
            return View(responceStatusViewModel);
        }

        public async Task<IActionResult> AllEmailSenders(ResponceStatusViewModel responceStatusViewModel)
        {
            CombinedAllEmailSendersViewModel combinedAllEmailSendersViewModel = new CombinedAllEmailSendersViewModel();
            combinedAllEmailSendersViewModel.Responce = responceStatusViewModel;
            try
            {
                var emails = await _emailSenderService.GetEmailSenders();
                combinedAllEmailSendersViewModel.EmailSenders = emails;
            }
            catch (Exception ex)
            {
                combinedAllEmailSendersViewModel.Responce.ErrorMessage += " " + ex.Message;
            }
            return View(combinedAllEmailSendersViewModel);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllUsers([FromBody]ResponceStatusViewModel responceStatus)
        {
            CombinedAllUsersViewModel combinedAllUsersViewModel = new CombinedAllUsersViewModel();
            combinedAllUsersViewModel.Responce = responceStatus;
            try
            {
                combinedAllUsersViewModel.TechnoShopUsers = _mapper.Map<List<TechnoShopUserViewModel>>(await _adminService.AllUsers());
                var roles = await _adminService.AllRoles();
                roles.ForEach(q =>
                {
                    combinedAllUsersViewModel.Roles.Add(q.Name);
                });
            }
            catch (Exception ex)
            {
                combinedAllUsersViewModel.Responce.ErrorMessage = ex.Message;
            }
            return View(combinedAllUsersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(roleName) == false)
            {
                IdentityResult result = await _adminService.AddRole(roleName);
                if (result.Succeeded)
                {
                    responceStatusViewModel.SucessMessage = $"Роль {roleName} успешно добавлена!";
                }
                else
                {
                    responceStatusViewModel.ErrorMessage = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllRoles", responceStatusViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(roleId) == false)
            {
                IdentityResult result = await _adminService.DeleteRole(roleId);
                if (result.Succeeded)
                {
                    responceStatusViewModel.SucessMessage = "Роль успешно удалена!";
                }
                else
                {
                    responceStatusViewModel.ErrorMessage = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllRoles", responceStatusViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(userId) == false && String.IsNullOrEmpty(roleName) == false)
            {
                var result = await _adminService.AddRoleToUser(userId, roleName);
                if (result.Succeeded)
                {
                    responceStatusViewModel.SucessMessage = $"Пользователю успешно назначена роль {roleName}!";
                }
                else
                {
                    responceStatusViewModel.ErrorMessage = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllUsers", responceStatusViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoleFromUser(string userId, string roleName)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(userId) == false && String.IsNullOrEmpty(roleName) == false)
            {
                var result = await _adminService.DeleteRoleFromUser(userId, roleName);
                if (result.Succeeded)
                {
                    responceStatusViewModel.SucessMessage = $"Роль {roleName} успешно удалена у пользователя!";
                }
                else
                {
                    responceStatusViewModel.ErrorMessage = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllUsers", responceStatusViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmEmail(string userId)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(userId) == false)
            {
                var result = await _adminService.ConfirmEmail(userId);
                if (result.Succeeded)
                {
                    responceStatusViewModel.SucessMessage = $"Юзеру успешно подтверждена почта!";
                }
                else
                {
                    responceStatusViewModel.ErrorMessage = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllUsers", responceStatusViewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmailSender(string email, string password)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(email) == false && String.IsNullOrEmpty(password) == false)
            {
                try
                {
                    await _emailSenderService.AddEmailSender(email, password);
                    responceStatusViewModel.SucessMessage = $"{email} еmail sender был успешно добавлен!";
                }
                catch (Exception ex)
                {
                    responceStatusViewModel.ErrorMessage = ex.Message;
                }
            }
            return RedirectToAction("AllEmailSenders", responceStatusViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}