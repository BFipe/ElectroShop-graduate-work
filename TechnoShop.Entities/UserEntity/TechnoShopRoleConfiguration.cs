using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.UserEntity
{
    public class TechnoShopRoleConfiguration : IEntityTypeConfiguration<TechnoShopRole>
    {
        public void Configure(EntityTypeBuilder<TechnoShopRole> builder)
        {
        }
    }
}
