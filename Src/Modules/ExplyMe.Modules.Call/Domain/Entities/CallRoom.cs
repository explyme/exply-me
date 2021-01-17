using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.Call.Domain.Entities
{
    public class CallRoom : Entity
    {
        public CallRoom()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string TwilioSid { get; set; }
        public string TwilioUniqueName { get; set; }
        public DateTime ExpireAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
