using System;

namespace ExplyMe.Modules.Call.Domain.Entities
{
    public class CallRoomAllowedUsers
    {
        public Guid RoomId { get; set; }
        public long UserId { get; set; }
    }
}
