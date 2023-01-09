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
using TechnoShop.Entities.UserRoleEntity;

namespace TechnoShop.BusinessLayer.Services.AdminServiceData
{
    public class AdminService : IAdminService
    {
        private readonly RoleManager<TechnoShopRole> _roleManager;
        private readonly UserManager<TechnoShopUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(RoleManager<TechnoShopRole> roleManager, UserManager<TechnoShopUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<TechnoShopRole>> AllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<TechnoShopUserDto>> AllUsers()
        {
            List<TechnoShopUserDto> technoShopUserDtos = new();
            var users = await _userManager.Users.ToListAsync();
            users.ForEach(q =>
            {
                var technoShopUser = new TechnoShopUserDto()
                {
                    Email = q.Email,
                    Id = q.Id,
                };

                q.TechnoShopRoles.ForEach(j =>
                {
                    technoShopUser.Roles.Add(j.Name);
                });

                technoShopUserDtos.Add(technoShopUser);
            }
            );

            return technoShopUserDtos;
        }

        public async Task<IdentityResult> AddRole(string roleName)
        {
            return await _roleManager.CreateAsync(new TechnoShopRole(roleName));
        }

        public async Task<IdentityResult> DeleteRole(string roleId)
        {
            return await _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(roleId));
        }

        public async Task<IdentityResult> AddRoleToUser(string userId, string roleName)
        {
            return await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(userId), roleName);
        }

        public async Task<IdentityResult> DeleteRoleFromUser(string userId, string roleName)
        {
            return await _userManager.RemoveFromRoleAsync(await _userManager.FindByIdAsync(userId), roleName);
        }
    }
}
