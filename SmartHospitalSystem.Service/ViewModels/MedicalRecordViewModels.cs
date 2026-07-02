using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHospitalSystem.Service.ViewModels
{
    public class MedicalRecordViewModel
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime CreatedAt { get; set; }

        public int PatientId { get; set; }
        public string PatientFullName { get; set; }

        public int DoctorId { get; set; }
        public string DoctorFullName { get; set; }
    }

    public class MedicalRecordCreateViewModel
    {
        [Required(ErrorMessage = "التشخيص الطبي إجباري")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "الخطة العلاجية إجبارية")]
        public string Treatment { get; set; }

        [Required(ErrorMessage = "يجب تحديد المريض")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "يجب تحديد الطبيب")]
        public int DoctorId { get; set; }
    }
}