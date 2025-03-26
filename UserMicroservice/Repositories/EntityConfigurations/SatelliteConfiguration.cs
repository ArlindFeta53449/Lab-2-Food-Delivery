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
    public class SatelliteConfiguration : IEntityTypeConfiguration<Satellite>
    {
        public void Configure(EntityTypeBuilder<Satellite> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.isDeleted).HasColumnType("bit").IsRequired();

            builder.HasOne(c => c.Planet)
                .WithMany(p => p.Satellites)
                .HasForeignKey(c => c.PlanetId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
