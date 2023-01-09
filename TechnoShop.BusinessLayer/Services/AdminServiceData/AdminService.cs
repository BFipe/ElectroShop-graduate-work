using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.BusinessLayer.Dtos.AdminDtos;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.BusinessLayer.Services.AdminServiceData
{
    public class AdminService : IAdminService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<TechnoShopUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(RoleManager<IdentityRole> roleManager, UserManager<TechnoShopUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<IdentityRole>> AllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<TechnoShopUserDto>> AllUsers()
        {
            return _mapper.Map<List<TechnoShopUserDto>>(await _userManager.Users.ToListAsync());
        }

        public async Task<IdentityResult> AddRole(string roleName)
        {
            return await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task<IdentityResult> DeleteRole(string roleId)
        {
            return await _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(roleId));
        }
    }
}
