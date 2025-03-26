using Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EntityConfigurations
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.BirthYear).IsRequired().HasMaxLength(255);


            builder.HasMany(p => p.Movies)
                .WithOne(c => c.Director)
                .HasForeignKey(c => c.DirectorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

