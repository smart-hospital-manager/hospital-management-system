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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            // تحديد الـ Primary Key (اختياري لأن EF بيفهمه تلقائي، بس زيادة تأكيد للأركيتكتشر)
            builder.HasKey(p => p.Id);

            // إعدادات الأعمدة والـ Constraints
            builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.NationalId)
                   .IsRequired()
                   .HasMaxLength(14); // الرقم القومي 14 رقم

            builder.Property(p => p.Email)
                   .HasMaxLength(100);

            builder.Property(p => p.BloodType)
                   .HasMaxLength(5);

            builder.Property(p => p.Address)
                   .HasMaxLength(250);

            builder.Property(p => p.EmergencyContactName)
                   .HasMaxLength(100);

            builder.Property(p => p.EmergencyContactPhone)
                   .HasMaxLength(20);

            // إعداد الـ Global Query Filter للـ Soft Delete
            // السطر ده بيخلي أي Query على جدول المرضى يجيب فقط اللي IsDeleted بتوعهم بـ false تلقائياً!
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}