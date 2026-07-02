using AutoMapper;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Service.ViewModels;

namespace SmartHospitalSystem.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ==========================================
            // 1. خرائط تحويل الأقسام (Departments)
            // ==========================================
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<DepartmentCreateViewModel, Department>();

            // ==========================================
            // 2. خرائط تحويل الدكاترة (Doctors)
            // ==========================================
            CreateMap<Doctor, DoctorViewModel>()
                // سطر ذكي لربط اسم القسم تلقائياً من الـ Navigation Property
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<DoctorCreateViewModel, Doctor>();

            // ==========================================
            // 3. خرائط تحويل المرضى (Patients)
            // ==========================================
            CreateMap<Patient, PatientViewModel>();
            CreateMap<PatientCreateViewModel, Patient>();

            // ==========================================
            // 4. خرائط تحويل المواعيد (Appointments)
            // ==========================================
            CreateMap<Appointment, AppointmentViewModel>()
                // دمج الاسم الأول والأخير للمريض ليظهر كاسم كامل في شاشة المواعيد
                .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
                // دمج الاسم الأول والأخير للدكتور ليظهر كاسم كامل في شاشة المواعيد
                .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"))
                // جلب اسم القسم الطبي التابع له الموعد
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            CreateMap<AppointmentCreateViewModel, Appointment>();

            // ==========================================
            // 5. خرائط تحويل السجلات الطبية (Medical Records)
            // ==========================================
            CreateMap<MedicalRecord, MedicalRecordViewModel>()
                .ForMember(dest => dest.PatientFullName, opt => opt.MapFrom(src => $"{src.Patient.FirstName} {src.Patient.LastName}"))
                .ForMember(dest => dest.DoctorFullName, opt => opt.MapFrom(src => $"{src.Doctor.FirstName} {src.Doctor.LastName}"));
            CreateMap<MedicalRecordCreateViewModel, MedicalRecord>();

            // ==========================================
            // 6. خرائط تحويل الإشعارات (Notifications)
            // ==========================================
            CreateMap<Notification, NotificationViewModel>();
            CreateMap<NotificationCreateViewModel, Notification>();
        }
    }
}