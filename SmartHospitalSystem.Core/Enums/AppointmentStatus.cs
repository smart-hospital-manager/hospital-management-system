using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Enums
{
    public enum AppointmentStatus
    {
        Pending = 1,     // قيد الانتظار أو المراجعة
        Confirmed = 2,   // تم التأكيد من الاستقبال
        Cancelled = 3,   // تم الإلغاء
        Completed = 4    // انتهى الكشف ودخل للدكتور
    }
}
