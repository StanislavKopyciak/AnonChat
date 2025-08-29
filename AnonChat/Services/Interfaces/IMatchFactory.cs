using AnonChat.Models;

namespace AnonChat.Services.Interfaces
{
    public interface IMatchFactory
    {
        MatchModel CreateMatch(int user1Id, int user2Id, int categoryId);
    }
}