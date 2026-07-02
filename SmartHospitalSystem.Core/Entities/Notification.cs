using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }         // عنوان الإشعار
        public string Message { get; set; }       // نص الإشعار بالكامل
        public bool IsRead { get; set; } = false;  // حالة الإشعار (مقروء أو غير مقروء)

        // لتحديد مين اللي هتوصل له الرسالة بناءً على الـ Role بتاعه في السيستم
        // (Admin, Doctor, Nurse, Receptionist, Accountant)
        public string TargetRole { get; set; }

        // اختياري: لو الإشعار موجه لدكتور معين بالـ Id بتاعه مثلاً
        public int? TargetUserId { get; set; }
    }
}
