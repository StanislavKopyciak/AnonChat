using AnonChat.Data;
using AnonChat.Models;
using AnonChat.Models.Enum.User;

public class ChatStatusService
{
    private readonly AnonChatContext _context;

    public ChatStatusService(AnonChatContext context)
    {
        _context = context;
    }

    public bool IsCompanionOnline(int matchId, int currentUserId)
    {
        var match = _context.Matche.Find(matchId);
        if (match == null) return false;

        int companionId = match.User1Id == currentUserId ? match.User2Id : match.User1Id;
        var companion = _context.User.Find(companionId);
        return companion != null && companion.State == UserState.InChat;
    }
}