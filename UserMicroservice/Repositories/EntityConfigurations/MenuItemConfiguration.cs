using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityConfiguration
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.Image).IsRequired(false);
            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.HasMany(x => x.MenuItemOffers)
                .WithOne(x => x.MenuItem)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.CartMenuItems)
                .WithOne(x => x.MenuItem)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x=>x.OrderMenuItems)
                .WithOne(x=>x.MenuItem)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
