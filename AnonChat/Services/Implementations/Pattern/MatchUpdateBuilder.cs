using AnonChat.Models;

namespace AnonChat.Services.Implementations.Pattern
{
    public class MatchUpdateBuilder
    {
        private readonly MatchModel _match;

        public MatchUpdateBuilder(MatchModel match)
        {
            _match = match;
        }

        public MatchUpdateBuilder SetUser1Id(int user1Id)
        {
            _match.User1Id = user1Id;
            return this;
        }

        public MatchUpdateBuilder SetUser2Id(int user2Id)
        {
            _match.User2Id = user2Id;
            return this;
        }

        public MatchUpdateBuilder SetCreatedAt(DateTime createdAt)
        {
            _match.CreatedAt = createdAt;
            return this;
        }

        public MatchUpdateBuilder SetUser1(UserModel user1)
        {
            _match.User1 = user1;
            return this;
        }

        public MatchUpdateBuilder SetUser2(UserModel user2)
        {
            _match.User2 = user2;
            return this;
        }

        public MatchUpdateBuilder SetMessages(ICollection<MessageModel> messages)
        {
            _match.Messages = messages;
            return this;
        }

        public MatchModel Build() => _match;
    }
}