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
    public class CostumerConfiguration : IEntityTypeConfiguration<Costumer>
    {
        public void Configure(EntityTypeBuilder<Costumer> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.isActive).HasColumnType("bit").IsRequired();
            builder.Property(p => p.isDeleted).HasColumnType("bit").IsRequired();
        }

    }
}
