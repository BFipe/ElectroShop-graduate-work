using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.UserOrderEntity
{
    public class UserOrderConfiguration : IEntityTypeConfiguration<UserOrder>
    {
        public void Configure(EntityTypeBuilder<UserOrder> builder)
        {
            builder.HasKey(q => q.UserOrderId);
            builder.Property(q => q.DateCreated).IsRequired(true);
            builder.Property(q => q.OrderNumber).IsRequired(true);
            builder.Property(q => q.FullName).IsRequired(true).HasMaxLength(60);
            builder.Property(q => q.PhoneNumber).IsRequired(true);
            builder.Property(q => q.City).IsRequired(true).HasMaxLength(30);
            builder.Property(q => q.Street).IsRequired(true).HasMaxLength(40);
            builder.Property(q => q.HouseNumber).IsRequired(true).HasMaxLength(30);
            builder.Property(q => q.FlatNumber).IsRequired(false).HasMaxLength(30);
            builder.Property(q => q.Entrance).IsRequired(false).HasMaxLength(30);
            builder.Property(q => q.Floor).IsRequired(false).HasMaxLength(30);
            builder.Property(q => q.OrderComment).IsRequired(false).HasMaxLength(150);
            builder.Property(q => q.OrderStatusComment).IsRequired(false).HasMaxLength(150);
            builder.Property(q => q.OrderStatus).IsRequired(true);
            builder.Property(q => q.OrderCompletionDate).IsRequired(false);

            builder
                .HasOne(q => q.TechnoShopUser)
                .WithMany(q => q.UserOrders)
                .HasForeignKey(q => q.TechnoShopUserId);

            builder.HasMany(q => q.Products)
                .WithMany(q => q.UserOrders)
                .UsingEntity<UserOrderProduct>(
                j =>
                j.HasOne(k => k.Product).WithMany(k => k.UserOrderProducts).HasForeignKey(k => k.ProductId),
                j => 
                j.HasOne(k => k.UserOrder).WithMany(k => k.UserOrderProducts).HasForeignKey(k => k.UserOrderId),
                j =>
                {
                    j.Property(q => q.ProductCount).HasDefaultValue(1);
                    j.HasKey(q => new { q.ProductId, q.UserOrderId });
                    j.ToTable("UserOrderProducts");
                }
                );
        }
    }
}
