using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntityConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Menu> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();
            builder.Property(x => x.Image).IsRequired(false);
            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.HasMany(x=>x.MenuItems)
                .WithOne(x => x.Menu)
                .HasForeignKey(x => x.MenuId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
