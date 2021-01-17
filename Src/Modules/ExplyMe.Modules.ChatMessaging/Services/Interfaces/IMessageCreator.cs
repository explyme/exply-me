using ExplyMe.Modules.ChatMessaging.Domain.Entities;
using System.Threading.Tasks;

namespace ExplyMe.Modules.ChatMessaging.Services.Interfaces
{
    public interface IMessageCreator
    {
        Task SendAsync(MessageEntity message);
    }
}
