  
using AutoMapper;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AppointmentViewModel>> GetAllAppointmentsAsync()
        {
            // بنجيب المواعيد وبنربط معاها المريض والدكتور والقسم
            var appointments = await _unitOfWork.Appointments.FindAsync(
                a => true,
                new string[] { "Patient", "Doctor", "Department" }
            );
            return _mapper.Map<List<AppointmentViewModel>>(appointments.ToList());
        }

        public async Task<AppointmentViewModel?> GetAppointmentByIdAsync(int id)
        {
            var appointments = await _unitOfWork.Appointments.FindAsync(
                a => a.Id == id,
                new string[] { "Patient", "Doctor", "Department" }
            );
            var appointment = appointments.FirstOrDefault();
            if (appointment == null) return null;

            return _mapper.Map<AppointmentViewModel>(appointment);
        }

        public async Task<List<AppointmentViewModel>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            var appointments = await _unitOfWork.Appointments.FindAsync(
                a => a.DoctorId == doctorId,
                new string[] { "Patient", "Department" }
            );
            return _mapper.Map<List<AppointmentViewModel>>(appointments.ToList());
        }

        public async Task AddAppointmentAsync(AppointmentCreateViewModel model)
        {
            var entity = _mapper.Map<Appointment>(model);
            await _unitOfWork.Appointments.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }
    }
}

