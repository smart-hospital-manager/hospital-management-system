using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Entities
{
    public class MedicalRecord : BaseEntity
    {
        public string Diagnosis { get; set; }      // التشخيص الطبي
        public string Treatment { get; set; }      // العلاج الموصوف (الروشتة)
        public string? Notes { get; set; }         // ملاحظات إضافية (nullable لو مفيش ملاحظات)

        // ربط السجل الطبي بالمريض
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        // ربط السجل الطبي بالدكتور اللي كتبه
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
