using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // تعريف الـ Properties للـ Repositories المختلفة
        public IPatientRepository Patients { get; private set; }
        public IDoctorRepository Doctors { get; private set; }
        public IAppointmentRepository Appointments { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public IMedicalRecordRepository MedicalRecords { get; private set; }
        public INotificationRepository Notifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // كبسلة وعمل Instance من كل Repository وتمرير الـ Context ليها
            Patients = new PatientRepository(_context);
            Doctors = new DoctorRepository(_context);
            Appointments = new AppointmentRepository(_context);
            Departments = new DepartmentRepository(_context);
            MedicalRecords = new MedicalRecordRepository(_context);
            Notifications = new NotificationRepository(_context);
        }

        // حفظ التغييرات كلها دفعة واحدة في قاعدة البيانات (Transaction)
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // تنظيف الـ Memory وقفل الاتصال بالداتابيز فور انتهاء الـ Request
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
