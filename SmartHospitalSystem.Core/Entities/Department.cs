using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } // اسم القسم
        public string Description { get; set; } // وصف القسم أو مكانه في المستشفى

        #region Navigation Properties (العلاقات)

        // القسم الواحد جواه لستة دكاترة شغالة فيه
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();

        // القسم بيحجز فيه مواعيد كتير
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        #endregion
    }
}
