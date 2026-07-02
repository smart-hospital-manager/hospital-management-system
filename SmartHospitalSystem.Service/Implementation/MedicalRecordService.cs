using AutoMapper;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Implementation
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalRecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MedicalRecordViewModel>> GetPatientHistoryAsync(int patientId)
        {
            var records = await _unitOfWork.MedicalRecords.FindAsync(
                r => r.PatientId == patientId,
                new string[] { "Doctor", "Patient" }
            );
            return _mapper.Map<List<MedicalRecordViewModel>>(records.ToList());
        }

        public async Task AddRecordAsync(MedicalRecordCreateViewModel model)
        {
            var entity = _mapper.Map<MedicalRecord>(model);
            await _unitOfWork.MedicalRecords.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }
    }
}
