

namespace AnonChat.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public int MatchId { get; set; }

        public string? Text { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;

        public UserModel? Sender { get; set; }
        public MatchModel? Match { get; set; }
    }
}
