using ExplyMe.Modules.Notification.Domain.Entities;
using ExplyMe.Modules.Notification.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Notification.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationEntity> GetAsync(Guid notificationId);
        Task SendAsync(NotificationTypeEnum notificationType, long recipientId, string url, object data);
        Task<IEnumerable<NotificationEntity>> FindNotificationsByUserIdAsync(long userId);
        Task DeleteAsync(Guid notificationId);
    }
}
