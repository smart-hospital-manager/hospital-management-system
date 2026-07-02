using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHospitalSystem.Core.Entities;
using System;

namespace SmartHospitalSystem.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.ReasonForVisit)
                   .IsRequired()
                   .HasMaxLength(250);

            // الـ Enum الخاص بحالة الحجز (سيبناه الافتراضي Int لتوفير المساحة)
            builder.Property(a => a.Status)
                   .IsRequired();

            // علاقة الموعد مع المريض
            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // التعديل هنا: تصحيح الـ Lambda Expression الخاصة بالـ ForeignKey للدكتور
            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId) // تم التصليح هنا من d.DoctorId إلى a.DoctorId
                   .OnDelete(DeleteBehavior.Restrict);

            // علاقة الموعد مع القسم
            builder.HasOne(a => a.Department)
                   .WithMany(dept => dept.Appointments)
                   .HasForeignKey(a => a.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // فلتر الـ Soft Delete
            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}