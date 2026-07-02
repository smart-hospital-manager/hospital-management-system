using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TargetRole { get; set; }
    }

    public class NotificationCreateViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string TargetRole { get; set; } // مثلاً يروح للـ Doctors كلهم أو الـ Patients
    }
}