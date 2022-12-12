using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.CartEntity;

namespace TechnoShop.Entities.ProductEntity
{
    public class UserCartConfiguration : IEntityTypeConfiguration<UserCart>
    {
        public void Configure(EntityTypeBuilder<UserCart> builder)
        {

        }
    }
}
