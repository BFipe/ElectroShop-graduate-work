using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechnoShop.Entities.UserEntity;
using TechnoShop.Models;

namespace TechnoShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<TechnoShopUser> _userManager;


        public AdminController(ILogger<AdminController> logger, RoleManager<IdentityRole> roleManager, UserManager<TechnoShopUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllRoles()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        [HttpPost]
        public async  Task<IActionResult> AddRole(string roleName)
        {
            if(String.IsNullOrEmpty(roleName) == false)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded == false)
                {
                    ViewData["Error"] = result.Errors;
                }
            }
            return RedirectToAction("AllRoles");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            if (String.IsNullOrEmpty(roleId) == false)
            {
                IdentityResult result = await _roleManager.DeleteAsync(await _roleManager.Roles.SingleAsync(q => q.Id == roleId));
                if (result.Succeeded == false)
                {
                    ViewData["Error"] = result.Errors;
                }
            }
            return RedirectToAction("AllRoles");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}