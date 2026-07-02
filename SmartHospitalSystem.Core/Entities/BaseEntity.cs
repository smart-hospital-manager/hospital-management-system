using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Entities
{
    public abstract class BaseEntity
    {
        // المعرف الفريد لكل السجلات في قاعدة البيانات
        public int Id { get; set; }

        // تاريخ ووقت إنشاء السجل تلقائياً
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // تاريخ ووقت آخر تحديث تم على السجل (قابل للنقاص لأنه مش دايماً بيتعدل)
        public DateTime? UpdatedAt { get; set; }

        // خاصية الـ Soft Delete عشان الداتا متمسحش نهائي من الداتابيز
        public bool IsDeleted { get; set; } = false;
    }
}
