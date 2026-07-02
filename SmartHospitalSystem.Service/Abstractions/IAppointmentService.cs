using SmartHospitalSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Abstractions
{
    public interface IAppointmentService
    {
        Task<List<AppointmentViewModel>> GetAllAppointmentsAsync();
        Task<AppointmentViewModel?> GetAppointmentByIdAsync(int id);
        Task<List<AppointmentViewModel>> GetAppointmentsByDoctorAsync(int doctorId);
        Task AddAppointmentAsync(AppointmentCreateViewModel model);
    }
}
