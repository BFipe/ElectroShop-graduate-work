using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.CartEntity;
using TechnoShop.Entities.ProductEntity;
using TechnoShop.Entities.UserRoleEntity;

namespace TechnoShop.Entities.UserEntity
{
    public class TechnoShopUserConfiguration : IEntityTypeConfiguration<TechnoShopUser>
    {
        public void Configure(EntityTypeBuilder<TechnoShopUser> builder)
        {
            builder
                .HasMany(q => q.Products)
                .WithMany(q => q.TechnoShopUsers)
                .UsingEntity<UserCart>( 
                j => j.HasOne(q => q.Product).WithMany(q => q.UserCarts).HasForeignKey(q => q.ProductId),
                j => j.HasOne(q => q.TechnoShopUser).WithMany(q => q.UserCarts).HasForeignKey(q => q.TechnoShopUserId),
                j =>
                {
                    j.Property(q => q.ProductCount).HasDefaultValue(1);
                    j.HasKey(q => new { q.TechnoShopUserId, q.ProductId });
                    j.ToTable("UserCart");
                });

            builder
                .HasMany(q => q.TechnoShopRoles)
                .WithMany(q => q.TechnoShopUsers)
                .UsingEntity<TechnoShopUserRole>();
        }
    }
}
