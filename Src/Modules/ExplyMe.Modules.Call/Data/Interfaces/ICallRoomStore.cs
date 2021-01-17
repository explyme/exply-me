using ExplyMe.Modules.Call.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Call.Data.Interfaces
{
    public interface ICallRoomStore
    {
        Task<CallRoom> GetAsync(Guid roomId);
        Task CreateRoom(CallRoom room);
        Task AssociateUserToRoom(Guid roomId, long userId);
    }
}
