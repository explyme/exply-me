using ExplyMe.Infrastructure.Data;
using ExplyMe.Modules.Notification.Domain.Enums;
using System;

namespace ExplyMe.Modules.Notification.Domain.Entities
{
    public class NotificationEntity : Entity
    {
        public NotificationEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReadAt { get; set; }
        public NotificationTypeEnum NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
        public long RecipientId { get; set; }
        public string Url { get; set; }
        public string CompiledMessage { get; set; }
    }
}
