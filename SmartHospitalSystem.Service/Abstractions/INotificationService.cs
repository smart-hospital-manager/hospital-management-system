using SmartHospitalSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Abstractions
{
    public interface INotificationService
    {
        Task<List<NotificationViewModel>> GetNotificationsByRoleAsync(string role);
        Task SendNotificationAsync(NotificationCreateViewModel model);
    }
}