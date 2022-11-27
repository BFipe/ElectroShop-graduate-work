using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.ProductEntity
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(q => q.ProductTypeId);
            builder.Property(q => q.TypeName).HasMaxLength(60).IsRequired();

            builder
                .HasMany(q => q.Products)
                .WithOne(q => q.ProductType)
                .HasForeignKey(q => q.ProductTypeName)
                .HasPrincipalKey(q => q.TypeName);
        }
    }
}
