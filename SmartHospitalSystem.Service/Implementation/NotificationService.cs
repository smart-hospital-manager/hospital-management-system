using AutoMapper;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<NotificationViewModel>> GetNotificationsByRoleAsync(string role)
        {
            var notifications = await _unitOfWork.Notifications.FindAsync(n => n.TargetRole == role);
            return _mapper.Map<List<NotificationViewModel>>(notifications.ToList());
        }

        public async Task SendNotificationAsync(NotificationCreateViewModel model)
        {
            var entity = _mapper.Map<Notification>(model);
            await _unitOfWork.Notifications.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }
    }
}
