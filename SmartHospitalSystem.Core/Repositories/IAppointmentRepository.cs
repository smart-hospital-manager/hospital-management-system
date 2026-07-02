using SmartHospitalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Repositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        // جلب المواعيد شاملة بيانات المريض والدكتور والقسم معاً
        Task<List<Appointment>> GetAppointmentsWithDetailsAsync();
    }
}
