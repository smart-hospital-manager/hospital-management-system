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
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DoctorViewModel>> GetAllDoctorsAsync()
        {
            // جلب الدكاترة من قاعدة البيانات
            var doctors = await _unitOfWork.Doctors.GetAllAsync();
            return _mapper.Map<List<DoctorViewModel>>(doctors);
        }

        public async Task<DoctorViewModel?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null) return null;

            return _mapper.Map<DoctorViewModel>(doctor);
        }

        public async Task<List<DoctorViewModel>> GetDoctorsByDepartmentAsync(int departmentId)
        {
            // التعديل هنا: تمرير اسم جدول الـ Department كـ string array متوافق مع الـ Generic الجديد
            var doctors = await _unitOfWork.Doctors.FindAsync(
                d => d.DepartmentId == departmentId && !d.IsDeleted,
                new string[] { "Department" }
            );

            // تحويل النتيجة إلى List لتتوافق مع الـ AutoMapper والـ Return Type
            var doctorsList = doctors.ToList();
            return _mapper.Map<List<DoctorViewModel>>(doctorsList);
        }

        public async Task AddDoctorAsync(DoctorCreateViewModel model)
        {
            var doctorEntity = _mapper.Map<Doctor>(model);
            await _unitOfWork.Doctors.AddAsync(doctorEntity);
            await _unitOfWork.CompleteAsync(); // حفظ حقيقي في الداتابيز
        }
    }
}