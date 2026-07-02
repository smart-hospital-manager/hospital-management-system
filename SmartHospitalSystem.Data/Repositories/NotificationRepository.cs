using Microsoft.EntityFrameworkCore;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Data.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        // جلب الإشعارات غير المقروءة للـ Role المعين لعرضها في جرس الإشعارات في الـ Layout
        public async Task<List<Notification>> GetUnreadNotificationsByRoleAsync(string role)
        {
            return await _context.Notifications
                .Where(n => n.TargetRole == role && !n.IsRead)
                .ToListAsync();
        }
    }
}
