using MedicationMockup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.EntityFrameworkCore.EntityConfiguration
{
    internal class MedicationEntityTypeConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {

            builder.ToTable("Medications");
            builder.HasKey(key => key.Id);
            builder.ToTable("Medications")
                .HasOne(one => one.CreatedBy)
                .WithMany(many => many.CreatedMedications)
                .HasForeignKey(fk => fk.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
            //builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.ToTable("Medications");
            builder.HasKey(key => key.Id);
            builder.ToTable("Medications")
                .HasOne(one => one.UpdatedBy)
                .WithMany(many => many.UpdatedMedications)
                .HasForeignKey(fk => fk.UpdatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Medications");
            builder.HasKey(key => key.Id);
            builder.ToTable("Medications")
                .HasOne(one => one.DeletedBy)
                .WithMany(many => many.DeletedMedications)
                .HasForeignKey(fk => fk.DeletedById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
