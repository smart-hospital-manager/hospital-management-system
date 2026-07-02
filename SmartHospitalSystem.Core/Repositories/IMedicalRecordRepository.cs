using SmartHospitalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Repositories
{
    public interface IMedicalRecordRepository : IGenericRepository<MedicalRecord>
    {
        // جلب السجلات الطبية الخاصة بمريض معين
        Task<List<MedicalRecord>> GetRecordsByPatientIdAsync(int patientId);
    }
}
