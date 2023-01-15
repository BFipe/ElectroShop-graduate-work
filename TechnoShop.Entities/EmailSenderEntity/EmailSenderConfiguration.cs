using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Entities.EmailSenderEntity
{
    public class EmailSenderConfiguration : IEntityTypeConfiguration<EmailSender>
    {
        public void Configure(EntityTypeBuilder<EmailSender> builder)
        {
            builder.HasKey(q => q.EmailSenderId);
            builder.Property(q => q.Password).HasMaxLength(24).IsRequired(true);
            builder.Property(q => q.Email).HasMaxLength(60).IsRequired(true);
            builder.HasIndex(q => q.Email).IsUnique(true);
        }
    }
}
