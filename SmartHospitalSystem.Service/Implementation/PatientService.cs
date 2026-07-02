using AutoMapper;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using System.Collections.Generic;
using System.Linq; // عشان الـ Any() والـ First() يشتغلوا بدون مشاكل
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PatientViewModel>> GetAllPatientsAsync()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            return _mapper.Map<List<PatientViewModel>>(patients);
        }

        public async Task<PatientViewModel?> GetPatientByIdAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null) return null;

            return _mapper.Map<PatientViewModel>(patient);
        }

        public async Task<PatientViewModel?> GetPatientByNationalIdAsync(string nationalId)
        {
            // استدعاء الـ FindAsync الجديد وتمرير نل للـ Includes لأننا لا نحتاج ربط جداول هنا
            var patients = await _unitOfWork.Patients.FindAsync(p => p.NationalId == nationalId && !p.IsDeleted, null);

            if (patients == null || !patients.Any()) return null;

            // أخذ أول مريض مطابق وتحويله للـ ViewModel
            return _mapper.Map<PatientViewModel>(patients.First());
        }

        public async Task AddPatientAsync(PatientCreateViewModel model)
        {
            var patientEntity = _mapper.Map<Patient>(model);
            await _unitOfWork.Patients.AddAsync(patientEntity);
            await _unitOfWork.CompleteAsync();
        }
    }
}