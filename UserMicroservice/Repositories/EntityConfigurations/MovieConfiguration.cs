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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(255);
            builder.Property(c => c.ReleaseYear).IsRequired().HasMaxLength(255);

            builder.HasOne(c => c.Director)
                .WithMany(p => p.Movies)
                .HasForeignKey(c => c.DirectorId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
