using System;

namespace ExplyMe.Modules.ChatMessaging.Domain.Entities
{
    public class MessageEntity
    {
        public MessageEntity()
        {
            CreatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public long FromUser { get; set; }
        public long ToUser { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
