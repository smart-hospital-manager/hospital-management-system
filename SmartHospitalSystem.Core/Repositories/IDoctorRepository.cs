using SmartHospitalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Repositories
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        // جلب الدكاترة مع الأقسام بتاعتهم عشان نعرض اسم القسم في الواجهة
        Task<List<Doctor>> GetDoctorsWithDepartmentsAsync();
    }
}
