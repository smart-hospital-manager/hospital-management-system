using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Entities
{
    public class Doctor : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public string Specialization { get; set; } // التخصص (مثلاً: باطنة، أطفال...)
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public decimal ConsultationFee { get; set; } // سعر الكشف
        public string Biography { get; set; } // نبذة عن الدكتور

        // ربط الدكتور بالقسم (كل دكتور ينتمي لقسم واحد)
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        #region Navigation Properties (العلاقات)

        // الدكتور عنده لستة مواعيد ولستة سجلات طبية بيكتبها للمرضى
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

        #endregion
    }
}
