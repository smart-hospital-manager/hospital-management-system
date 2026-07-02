using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHospitalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Data.Configurations
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(mr => mr.Id);

            builder.Property(mr => mr.Diagnosis)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(mr => mr.Treatment)
                   .IsRequired()
                   .HasMaxLength(1000); // الروشتة ممكن تكون طويلة

            builder.Property(mr => mr.Notes)
                   .HasMaxLength(500);

            // علاقة السجل مع المريض
            builder.HasOne(mr => mr.Patient)
                   .WithMany(p => p.MedicalRecords)
                   .HasForeignKey(mr => mr.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // علاقة السجل مع الدكتور
            builder.HasOne(mr => mr.Doctor)
                   .WithMany(d => d.MedicalRecords)
                   .HasForeignKey(mr => mr.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // فلتر الـ Soft Delete
            builder.HasQueryFilter(mr => !mr.IsDeleted);
        }
    }
}
