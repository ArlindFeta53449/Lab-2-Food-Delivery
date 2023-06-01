using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.RoleId)
                    .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired();
            builder.Property(c=>c.Surname)
                .IsRequired();
            builder.Property(c=>c.IsEmailVerified)
                .HasDefaultValue(false);
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x=>x.UserId);
            builder.HasIndex(x => x.Email)
                .IsUnique();
            builder.Property(x => x.Email)
                .IsRequired();
            builder.Property(x => x.StripeCustomerId)
                .IsRequired(false);
        }
    }
}
