using AnonChat.Services.Interfaces;
using AnonChat.Models;

namespace AnonChat.Services.Implementations.Pattern
{
    public class NormalMatchFactory : IMatchFactory
    {
        public MatchModel CreateMatch(int user1Id, int user2Id, int categoryId)
        {
            return new MatchModel
            {
                User1Id = user1Id,
                User2Id = user2Id,
                CreatedAt = DateTime.Now
            };
        }
    }
}