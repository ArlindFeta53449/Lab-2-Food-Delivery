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
    public class CartOfferConfiguration : IEntityTypeConfiguration<CartOffer>
    {
        public void Configure(EntityTypeBuilder<CartOffer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Offer)
                   .WithMany(x => x.CartOffers)
                   .HasForeignKey(x => x.OfferId);

            builder.HasOne(x => x.Cart)
                   .WithMany(x => x.CartOffers)
                   .HasForeignKey(x => x.CartId);
            builder.Property(x => x.Quantity)
                .IsRequired();
        }
    }
}
