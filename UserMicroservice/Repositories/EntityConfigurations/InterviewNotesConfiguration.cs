using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Repositories.EntityConfigurations
{
    public class InterviewNotesConfiguration : IEntityTypeConfiguration<InterviewNotes>
    {
        public void Configure(EntityTypeBuilder<InterviewNotes> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.note).IsRequired().HasMaxLength(255);
            builder.Property(p => p.isPassed).HasColumnType("bit").IsRequired();

            builder.HasOne(c => c.Interview)
                .WithMany(p => p.InterviewNotes)
                .HasForeignKey(c => c.InterviewId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
