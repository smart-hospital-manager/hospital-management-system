using Microsoft.EntityFrameworkCore;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }

        // جلب الدكاترة مع الأقسام بتاعتهم عشان تظهر في الـ Index
        public async Task<List<Doctor>> GetDoctorsWithDepartmentsAsync()
        {
            return await _context.Doctors
                .Include(d => d.Department)
                .ToListAsync();
        }
    }
}
