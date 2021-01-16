using ExplyMe.Modules.Notification.Data.Interface;
using ExplyMe.Modules.Notification.Domain.Enums;
using ExplyMe.Modules.Notification.Services.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Notification.Services
{
    public class NotificationMessageCompiler : INotificationMessageCompiler
    {
        public INotificationTypeRepository NotificationTypeRepository { get; }

        public NotificationMessageCompiler(
            INotificationTypeRepository notificationTypeRepository)
        {
            NotificationTypeRepository = notificationTypeRepository ?? throw new ArgumentNullException(nameof(notificationTypeRepository));
        }

        public async Task<string> CompileAsync(NotificationTypeEnum notificationTypeId, object data)
        {
            var notificationType = await NotificationTypeRepository.GetAsync(notificationTypeId);
            var pattern = new Regex(@"\{\{\w*\}\}");
            var matches = pattern.Matches(notificationType.Template);

            foreach (Match match in matches)
            {
                var normalizedKey = match.Value.Replace("{{", string.Empty).Replace("}}", string.Empty);

                string GetValueFromData()
                {
                    var property = data.GetType().GetProperty(normalizedKey);

                    return property is null
                        ? string.Concat("{{", normalizedKey, "}}")
                        : property.GetValue(data, null) as string;
                }

                notificationType.Template = notificationType.Template.Replace(match.Value, GetValueFromData());
            }

            return notificationType.Template;
        }
    }
}
