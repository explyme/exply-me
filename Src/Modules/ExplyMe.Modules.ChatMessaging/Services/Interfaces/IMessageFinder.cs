using ExplyMe.Modules.ChatMessaging.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExplyMe.Modules.ChatMessaging.Services.Interfaces
{
    public interface IMessageFinder
    {
        Task<IEnumerable<MessageEntity>> FindAllByFromUserIdAndToUserId(long fromUserId, long toUserId);
    }
}
