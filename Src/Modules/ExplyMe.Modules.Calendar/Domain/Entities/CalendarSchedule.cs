using ExplyMe.Modules.Calendar.Domain.Enums;
using System;

namespace ExplyMe.Modules.Calendar.Domain.Entities
{
    public class CalendarSchedule
    {
        public Guid Id { get; set; }
        public long RequestorId { get; set; }
        public long SlotId { get; set; }
        public CalendarScheduleEnum Status { get; set; }
        public byte StartHour { get; set; }
        public byte EndHour { get; set; }
    }
}
