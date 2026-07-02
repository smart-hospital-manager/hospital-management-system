using SmartHospitalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Repositories
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        // هنا نقدر نضيف أي طريقة بحث خاصة بالمريض مستقبلاً (مثل البحث بالرقم القومي)
        Task<Patient?> GetPatientWithMedicalRecordsAsync(int id);
    }
}