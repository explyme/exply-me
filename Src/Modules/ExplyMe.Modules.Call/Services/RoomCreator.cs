using ExplyMe.Infrastructure.Services.Interfaces;
using ExplyMe.Modules.Call.Data.Interfaces;
using ExplyMe.Modules.Call.Domain.Entities;
using ExplyMe.Modules.Call.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Twilio.Rest.Video.V1;

namespace ExplyMe.Modules.Call.Services
{
    public class RoomCreator : IRoomCreator
    {
        public ICallRoomStore CallRoomStore { get; }
        public IAppSettingsService AppSettingsService { get; }

        public RoomCreator(
            ICallRoomStore callRoomStore,
            IAppSettingsService appSettingsService)
        {
            CallRoomStore = callRoomStore ?? throw new ArgumentNullException(nameof(callRoomStore));
            AppSettingsService = appSettingsService ?? throw new ArgumentNullException(nameof(appSettingsService));
        }

        public async Task<CallRoom> CreateP2PAsync(
            DateTime expireAt,
            long user1,
            long user2)
        {
            var twilioRoom = await CreateTwilioRoom();

            var room = new CallRoom
            {
                ExpireAt = expireAt,
                TwilioSid = twilioRoom.UniqueName,
                TwilioUniqueName = twilioRoom.Sid
            };

            await CallRoomStore.CreateRoom(room);
            await CallRoomStore.AssociateUserToRoom(room.Id, user1);
            await CallRoomStore.AssociateUserToRoom(room.Id, user2);

            return room;
        }

        private async Task<RoomResource> CreateTwilioRoom()
        {
            var room = await RoomResource.CreateAsync(new CreateRoomOptions
            {
                EnableTurn = true,
                MaxParticipants = 2,
                RecordParticipantsOnConnect = false,
                Type = RoomResource.RoomTypeEnum.PeerToPeer,
                UniqueName = Guid.NewGuid().ToString()
            });
            return room;
        }
    }
}
