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

        public async Task<IActionResult> AllEmailSenders()
        {
            CombinedAllEmailSendersViewModel combinedAllEmailSendersViewModel = new CombinedAllEmailSendersViewModel();
            combinedAllEmailSendersViewModel.Responce.SucessMessage = TempData["SuccessMessage"] as string;
            combinedAllEmailSendersViewModel.Responce.ErrorMessage = TempData["ErrorMessage"] as string;
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
        public async Task<IActionResult> AllRoles()
        {
            CombinedAllRolesViewModel combinedAllRolesViewModel = new CombinedAllRolesViewModel();
            combinedAllRolesViewModel.Responce.SucessMessage = TempData["SuccessMessage"] as string;
            combinedAllRolesViewModel.Responce.ErrorMessage = TempData["ErrorMessage"] as string;
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
        public async Task<IActionResult> AllUsers()
        {
            CombinedAllUsersViewModel combinedAllUsersViewModel = new CombinedAllUsersViewModel();
            combinedAllUsersViewModel.Responce.SucessMessage = TempData["SuccessMessage"] as string;
            combinedAllUsersViewModel.Responce.ErrorMessage = TempData["ErrorMessage"] as string;
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
        public async Task<IActionResult> AddRole([FromForm] string roleName)
        {
            if (String.IsNullOrEmpty(roleName) == false)
            {
                IdentityResult result = await _adminService.AddRole(roleName);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"Роль {roleName} успешно добавлена!";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllRoles");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole([FromForm] string roleId)
        {
            ResponceStatusViewModel responceStatusViewModel = new ResponceStatusViewModel();
            if (String.IsNullOrEmpty(roleId) == false)
            {
                IdentityResult result = await _adminService.DeleteRole(roleId);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Роль успешно удалена!";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllRoles");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRoleToUser([FromForm] string userId, string roleName)
        {
            if (String.IsNullOrEmpty(userId) == false && String.IsNullOrEmpty(roleName) == false)
            {
                var result = await _adminService.AddRoleToUser(userId, roleName);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"Роль {roleName} успешно добавлена к пользователю!";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllUsers");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoleFromUser([FromForm] string userId, string roleName)
        {
            if (String.IsNullOrEmpty(userId) == false && String.IsNullOrEmpty(roleName) == false)
            {
                var result = await _adminService.DeleteRoleFromUser(userId, roleName);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"Роль {roleName} успешно удалена у пользователя!";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllUsers");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmEmail([FromForm] string userId)
        {
            if (String.IsNullOrEmpty(userId) == false)
            {
                var result = await _adminService.ConfirmEmail(userId);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"Юзеру успешно подтверждена почта!";
                }
                else
                {
                    TempData["ErrorMessage"] = result.Errors.FirstOrDefault().Description;
                }
            }
            return RedirectToAction("AllUsers");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmailSender([FromForm] string email, string password)
        {
            if (String.IsNullOrEmpty(email) == false && String.IsNullOrEmpty(password) == false)
            {
                try
                {
                    await _emailSenderService.AddEmailSender(email, password);
                    TempData["SuccessMessage"] = $"{email} еmail sender был успешно добавлен!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            return RedirectToAction("AllEmailSenders");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmailSender([FromForm] string email)
        {
            if (String.IsNullOrEmpty(email) == false)
            {
                try
                {
                    await _emailSenderService.DeleteEmailSender(email);
                    TempData["SuccessMessage"] = $"{email} еmail sender был успешно удален!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
            }
            return RedirectToAction("AllEmailSenders");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}