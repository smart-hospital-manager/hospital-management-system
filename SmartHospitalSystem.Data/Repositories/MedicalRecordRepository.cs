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
    public class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(ApplicationDbContext context) : base(context)
        {
        }

        // جلب تاريخ السجلات الطبية بالكامل لمريض معين يرتب من الأحدث للأقدم
        public async Task<List<MedicalRecord>> GetRecordsByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Where(mr => mr.PatientId == patientId)
                .Include(mr => mr.Doctor) // عشان نعرف مين الدكتور اللي كتب الروشتة دي
                .ToListAsync();
        }
    }
}
