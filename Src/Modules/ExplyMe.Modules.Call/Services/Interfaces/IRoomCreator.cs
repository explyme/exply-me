using ExplyMe.Modules.Call.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Call.Services.Interfaces
{
    public interface IRoomCreator
    {
        Task<CallRoom> CreateP2PAsync(DateTime expireAt, long user1, long user2);
    }
}
