using Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EntityConfigurations
{
    public class CartMenuItemConfiguration : IEntityTypeConfiguration<CartMenuItem>
    {
        public void Configure(EntityTypeBuilder<CartMenuItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.MenuItem)
                   .WithMany(x => x.CartMenuItems)
                   .HasForeignKey(x => x.MenuItemId);

            builder.HasOne(x => x.Cart)
                   .WithMany(x => x.CartMenuItems)
                   .HasForeignKey(x => x.CartId);
            builder.Property(x => x.Quantity)
                .IsRequired();
        }
    }
}
