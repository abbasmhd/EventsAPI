using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsAPI.Entities.Mapper
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {

            builder.ToTable("Events");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasDefaultValueSql("newsequentialid()");

            builder.HasIndex(u => u.Name).IsUnique(); // Ensure the Name is Unique.
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(c => c.Description)
                   .IsRequired();

            builder.Property(c => c.Timezone)
                   .HasMaxLength(255);

            builder.Property(c => c.StartDate)
                   .IsRequired();
            
            builder.Property(c => c.EndDate)
                   .IsRequired();
            
            builder.Property(c => c.CreatedBy)
                   .HasMaxLength(255);

            builder.Property(c => c.CreatedDate);

            builder.Property(c => c.ModifiedBy)
                   .HasMaxLength(255);

            builder.Property(c => c.ModifiedDate);

        }
    }
}
