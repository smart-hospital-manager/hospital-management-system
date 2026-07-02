using SmartHospitalSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Abstractions
{
    public interface IMedicalRecordService
    {
        Task<List<MedicalRecordViewModel>> GetPatientHistoryAsync(int patientId);
        Task AddRecordAsync(MedicalRecordCreateViewModel model);
    }
}
