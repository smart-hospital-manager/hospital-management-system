using SmartHospitalSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }

        // خليتهم ? عشان لو المريض مش فاكر بيانات الطوارئ وهو بيسجل أول مرة
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }

        #region Navigation Properties (العلاقات)

        // تحويل العلاقات إلى List عادية وصريحة
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

        #endregion
    }
}
