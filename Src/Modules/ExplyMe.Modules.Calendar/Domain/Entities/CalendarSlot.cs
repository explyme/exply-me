using ExplyMe.Infrastructure.Data;
using System;

namespace ExplyMe.Modules.Calendar.Domain.Entities
{
    public class CalendarSlot : Entity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public byte StartHour { get; set; }
        public byte EndHour { get; set; }
        public DayOfWeek? RepeatOn { get; set; }
        public DateTime StartAt { get; set; }
    }
}
