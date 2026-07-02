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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(dept => dept.Id);

            builder.Property(dept => dept.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(dept => dept.Description)
                   .HasMaxLength(500);

            // فلتر الـ Soft Delete
            builder.HasQueryFilter(dept => !dept.IsDeleted);
        }
    }
}