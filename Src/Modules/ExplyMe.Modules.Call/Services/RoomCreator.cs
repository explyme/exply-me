using ExplyMe.Modules.Call.Data.Interfaces;
using ExplyMe.Modules.Call.Domain.Entities;
using ExplyMe.Modules.Call.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Call.Services
{
    public class RoomCreator : IRoomCreator
    {
        public ICallRoomStore CallRoomStore { get; }

        public RoomCreator(
            ICallRoomStore callRoomStore)
        {
            CallRoomStore = callRoomStore ?? throw new ArgumentNullException(nameof(callRoomStore));
        }

        public async Task<CallRoom> CreateP2PAsync(
            DateTime expireAt, 
            long user1, 
            long user2)
        {
            var room = new CallRoom
            {
                ExpireAt = expireAt,
                TwilioSid = "",
                TwilioUniqueName = ""
            };

            await CallRoomStore.CreateRoom(room);
            await CallRoomStore.AssociateUserToRoom(room.Id, user1);
            await CallRoomStore.AssociateUserToRoom(room.Id, user2);

            return room;
        }
    }
}
