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
        }
    }
}
