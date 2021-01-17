using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.Calendar.Domain.Entities
{
    public class CalendarSlot : Entity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan RepeatInterval { get; set; }
        public DateTime StartAt { get; set; }
    }
}
