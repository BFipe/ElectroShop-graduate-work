using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TechnoShop.Data.Repositories;
using TechnoShop.Data.Repositories.Interfaces;

namespace TechnoShop.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, q => q.MigrationsAssembly("TechnoShop.Data"));
        });
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IEmailSenderServiceRepository, EmailSenderRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository> ();
        return services;
    }
}