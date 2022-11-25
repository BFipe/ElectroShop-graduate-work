using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TechnoShop.BusinessLayer.Extensions;
using TechnoShop.BusinessLayer.Interfaces;
using TechnoShop.BusinessLayer.Services.ProductServiceData;
using TechnoShop.Data;
using TechnoShop.Data.Extensions;
namespace TechnoShop.BusinessLayer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services, string connectionString)
    {
        services.AddDatabase(connectionString);
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddAutoMapper(Assembly.GetCallingAssembly(),
                       Assembly.GetExecutingAssembly());
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}