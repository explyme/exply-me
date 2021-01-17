using System;

namespace ExplyMe.Modules.Calendar.Domain
{
    public class SlotHour
    {
        public bool IsActive { get; set; }
        public TimeSpan Time { get; set; }
    }
}
