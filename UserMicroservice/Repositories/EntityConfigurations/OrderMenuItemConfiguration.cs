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
    public class OrderMenuItemConfiguration : IEntityTypeConfiguration<OrderMenuItem>
    {
        public void Configure(EntityTypeBuilder<OrderMenuItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.MenuItem)
                   .WithMany(x => x.OrderMenuItems)
                   .HasForeignKey(x => x.MenuItemId);

            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderMenuItems)
                   .HasForeignKey(x => x.OrderId);
        }
    }
}
