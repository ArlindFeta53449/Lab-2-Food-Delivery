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
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Amount).IsRequired();

            // Update the configuration for the Status property
            builder.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(255)
                .HasConversion<string>()
                .IsUnicode(false); // optional, ensures that the column is non-unicode

            builder.Property(p => p.isDeleted).HasColumnType("bit").IsRequired();

            builder.HasOne(c => c.Costumer)
                .WithMany(p => p.Loans)
                .HasForeignKey(c => c.CostumerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
