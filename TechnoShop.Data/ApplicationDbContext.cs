using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoShop.Entities.ProductEntity;
using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.UserEntity;

namespace TechnoShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TechnoShopUser> TechnoShopUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductTypeConfiguration());
            builder.ApplyConfiguration(new UserCartConfiguration());
            builder.ApplyConfiguration(new TechnoShopUserConfiguration());
        }
    }
}
