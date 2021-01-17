using ExplyMe.Infrastructure.Data;

namespace ExplyMe.Modules.Calendar.Domain.Entities
{
    public class CalendarSlotSchools : Entity
    {
        public long SlotId { get; set; }
        public long SchoolId { get; set; }
    }
}
