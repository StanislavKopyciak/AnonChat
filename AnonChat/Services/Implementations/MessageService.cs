using AnonChat.Data;
using AnonChat.Models;
using Microsoft.EntityFrameworkCore;

public class MessageService
{
    private readonly AnonChatContext _context;

    public MessageService(AnonChatContext context)
    {
        _context = context;
    }

    public async Task SendMessageAsync(int senderId, int matchId, string text)
    {
        var message = new MessageModel
        {
            SenderId = senderId,
            MatchId = matchId,
            Text = text
        };

        _context.Message.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MessageModel>> GetMessagesAsync(int matchId)
    {
        return await _context.Message
            .Where(m => m.MatchId == matchId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task DeleteMatchAsync(int matchId)
    {
        var match = await _context.Matche.FindAsync(matchId);
        if (match != null)
        {
            _context.Matche.Remove(match);
            await _context.SaveChangesAsync();
        }
    }
}
