using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.Notification.Domain.Entities
{
    public class NotificationSettings : Entity
    {
        public int Id { get; set; }
        public Guid NotificationId { get; set; }
        public bool SendMail { get; set; }
        public bool SendPush { get; set; }
        public bool Urgent { get; set; }
    }
}
