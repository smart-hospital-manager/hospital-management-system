using SmartHospitalSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Repositories
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        // جلب الإشعارات غير المقروءة الموجهة لـ Role معين
        Task<List<Notification>> GetUnreadNotificationsByRoleAsync(string role);
    }
}
