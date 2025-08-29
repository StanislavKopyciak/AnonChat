using AnonChat.Models;

namespace AnonChat.Services.Interface
{
    public interface IMessageService
    {
        Task SendMessageAsync(int matchId, int senderId, string text);

        Task<IEnumerable<MessageModel>> GetMessagesAsync(int matchId);

        Task DeleteMatchAsync(int matchId);
    }
}
