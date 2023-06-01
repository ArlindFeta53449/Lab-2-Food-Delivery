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
    public class OrderOfferConfiguration : IEntityTypeConfiguration<OrderOffer>
    {
        public void Configure(EntityTypeBuilder<OrderOffer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Offer)
                   .WithMany(x => x.OrderOffers)
                   .HasForeignKey(x => x.OfferId);

            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderOffers)
                   .HasForeignKey(x => x.OrderId);
        }
    
    }
}
