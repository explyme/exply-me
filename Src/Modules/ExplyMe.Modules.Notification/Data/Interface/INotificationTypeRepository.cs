using ExplyMe.Modules.Notification.Domain.Entities;
using ExplyMe.Modules.Notification.Domain.Enums;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Notification.Data.Interface
{
    public interface INotificationTypeRepository
    {
        Task<NotificationType> GetAsync(NotificationTypeEnum notificationType);
    }
}
