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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Specialization).IsRequired().HasMaxLength(100);
            builder.Property(d => d.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(d => d.NationalId).IsRequired().HasMaxLength(14);
            builder.Property(d => d.Biography).HasMaxLength(500);

            // تحديد نوع البيانات لـ سعر الكشف (decimal) عشان الدقة المالية في الـ SQL Server
            builder.Property(d => d.ConsultationFee)
                   .HasColumnType("decimal(18,2)");

            // إعداد العلاقة: القسم الواحد يحتوي على دكاترة كتير (One-to-Many)
            builder.HasOne(d => d.Department)
                   .WithMany(dept => dept.Doctors)
                   .HasForeignKey(d => d.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); // منع مسح القسم لو جواه دكاترة لحماية الداتا

            // فلتر الـ Soft Delete للدكاترة
            builder.HasQueryFilter(d => !d.IsDeleted);
        }
    }
}
