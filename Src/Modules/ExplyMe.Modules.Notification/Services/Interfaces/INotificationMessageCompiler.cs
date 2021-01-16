using ExplyMe.Modules.Notification.Domain.Enums;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Notification.Services.Interfaces
{
    public interface INotificationMessageCompiler
    {
        Task<string> CompileAsync(NotificationTypeEnum notificationTypeId, object data);
    }
}
