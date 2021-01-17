using ExplyMe.Modules.Calendar.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Calendar.Store.Interfaces
{
    public interface ICalendarSlotStore
    {
        Task CreateAsync(CalendarSlot slot);
        Task UpdateAsync(CalendarSlot slot);
        Task<CalendarSlot> GetAsync(long slotId);
        Task<IEnumerable<CalendarSlot>> FindByUser(long userId);
    }
}
