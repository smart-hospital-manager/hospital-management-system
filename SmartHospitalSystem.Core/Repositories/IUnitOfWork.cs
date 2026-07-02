using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Repositories
{
    // ترث من IDisposable عشان نقفل الاتصال بالداتا بيز فور انتهاء العملية وتوفير الـ Memory
    public interface IUnitOfWork : IDisposable
    {
        // تجميع كل الـ Repositories تحت إيد الـ Unit of Work
        IPatientRepository Patients { get; }
        IDoctorRepository Doctors { get; }
        IAppointmentRepository Appointments { get; }
        IDepartmentRepository Departments { get; }
        IMedicalRecordRepository MedicalRecords { get; }
        INotificationRepository Notifications { get; }

        // العملية اللي هتعمل حفظ حقيقي لكل التغييرات في قاعدة البيانات مرة واحدة Async
        Task<int> CompleteAsync();
    }
}
