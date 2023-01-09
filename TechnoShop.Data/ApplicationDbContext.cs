using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnoShop.Entities.ProductEntity;
using Microsoft.AspNetCore.Identity;
using TechnoShop.Entities.UserEntity;
using TechnoShop.Entities.UserOrderEntity;
using TechnoShop.Entities.EmailSenderEntity;
using TechnoShop.Entities.UserRoleEntity;

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
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<EmailSender> EmailSenders { get; set; }
        public DbSet<TechnoShopRole> TechnoShopRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductTypeConfiguration());
            builder.ApplyConfiguration(new TechnoShopUserConfiguration());
            builder.ApplyConfiguration(new UserOrderConfiguration());
            builder.ApplyConfiguration(new EmailSenderConfiguration());
        }
    }
}
