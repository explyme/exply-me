using ExplyMe.Infrastructure.Data;

namespace ExplyMe.Modules.Notification.Domain.Entities
{
    public class NotificationType : Entity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public string Icon { get; set; }
    }
}