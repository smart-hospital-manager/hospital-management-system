using SmartHospitalSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartHospitalSystem.Service.Abstractions
{
    public interface IDoctorService
    {
        Task<List<DoctorViewModel>> GetAllDoctorsAsync();
        Task<DoctorViewModel?> GetDoctorByIdAsync(int id);
        Task<List<DoctorViewModel>> GetDoctorsByDepartmentAsync(int departmentId);
        Task AddDoctorAsync(DoctorCreateViewModel model);
    }
}