using SmartHospitalSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public DateTime AppointmentDate { get; set; } // تاريخ ووقت الحجز
        public string ReasonForVisit { get; set; } // سبب الزيارة أو الشكوى
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending; // القيمة الافتراضية قيد الانتظار
        // ربط الموعد بالمريض
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        // ربط الموعد بالدكتور
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        // ربط الموعد بالقسم
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
