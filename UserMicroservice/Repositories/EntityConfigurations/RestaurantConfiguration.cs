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
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.Address)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .IsRequired();
            builder.Property(x => x.Image).IsRequired(false);
            builder.Property(x=>x.ImagePath).IsRequired(false);

            builder.HasMany(x=>x.Menus)
                .WithOne(x=>x.Restaurant)
                .HasForeignKey(x=>x.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x=>x.Offers)
                .WithOne(x=>x.Restaurant)
                .HasForeignKey(x=>x.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
