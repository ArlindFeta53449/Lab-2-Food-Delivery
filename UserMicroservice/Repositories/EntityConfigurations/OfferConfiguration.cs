using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityConfiguration
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Offer> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.DiscountPercent)
                .IsRequired();
            builder.Property(x => x.StartDate)
                .IsRequired();
            builder.Property(x => x.Image).IsRequired(false);
            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.HasMany(x => x.MenuItemOffers)
                .WithOne(x => x.Offer)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x=>x.CartOffers)
                .WithOne(x => x.Offer)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.OrderOffers)
                .WithOne(x => x.Offer)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
