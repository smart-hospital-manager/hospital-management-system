using SmartHospitalSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Abstractions
{
    public interface IPatientService
    {
        Task<List<PatientViewModel>> GetAllPatientsAsync();
        Task<PatientViewModel?> GetPatientByIdAsync(int id);
        Task<PatientViewModel?> GetPatientByNationalIdAsync(string nationalId); // ميزة البحث بالرقم القومي
        Task AddPatientAsync(PatientCreateViewModel model);
    }
}
