using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using SmartHospitalSystem.Core.Enums; // لو عندك Enum لحالة الموعد

namespace SmartHospitalSystem.Service.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ReasonForVisit { get; set; }
        public string Status { get; set; } // مثلاً: Pending, Confirmed, Canceled

        public int PatientId { get; set; }
        public string PatientFullName { get; set; }

        public int DoctorId { get; set; }
        public string DoctorFullName { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class AppointmentCreateViewModel
    {
        [Required(ErrorMessage = "يجب تحديد تاريخ ووقت الموعد")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "سبب الزيارة إجباري")]
        [StringLength(500)]
        public string ReasonForVisit { get; set; }

        [Required(ErrorMessage = "يجب اختيار المريض")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "يجب اختيار الطبيب")]
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "يجب اختيار القسم")]
        public int DepartmentId { get; set; }
    }
}
