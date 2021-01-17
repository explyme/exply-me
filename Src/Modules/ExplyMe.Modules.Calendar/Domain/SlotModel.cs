using System;

namespace ExplyMe.Modules.Calendar.Domain
{
    public class SlotModel
    {
        public TimeSpan? StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
