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
    public class MenuItemOfferConfiguration : IEntityTypeConfiguration<MenuItemOffer>
    {
        public void Configure(EntityTypeBuilder<MenuItemOffer> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.HasOne(x => x.MenuItem)
                   .WithMany(x => x.MenuItemOffers)
                   .HasForeignKey(x => x.MenuItemId);

            builder.HasOne(x => x.Offer)
                   .WithMany(x => x.MenuItemOffers)
                   .HasForeignKey(x => x.OfferId);
        }
    }
}
