using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using TechnoShop.BusinessLayer.Extensions;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.BusinessLayer.Services.AdminServiceData;
using TechnoShop.BusinessLayer.Services.CartServiceData;
using TechnoShop.BusinessLayer.Services.EmailSenderServiceData;
using TechnoShop.BusinessLayer.Services.ManagerServiceData;
using TechnoShop.BusinessLayer.Services.ProductServiceData;
using TechnoShop.Data;
using TechnoShop.Data.Extensions;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.BusinessLayer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services, string connectionString)
    {
        services.AddDatabase(connectionString);
        services.AddDefaultIdentity<TechnoShopUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<TechnoShopRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAutoMapper(Assembly.GetCallingAssembly(),
                       Assembly.GetExecutingAssembly());

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        services.AddScoped<IEmailSender, EmailSenderService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IAdminService, AdminService>();
        return services;
    }
}