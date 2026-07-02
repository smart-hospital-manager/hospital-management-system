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
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        // جلب تفاصيل الحجز كاملة (مريض ودكتور وقسم) في شاشة الاستقبال أو لوحة التحكم
        public async Task<List<Appointment>> GetAppointmentsWithDetailsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .ToListAsync();
        }
    }
}
