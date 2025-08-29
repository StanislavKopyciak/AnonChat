namespace AnonChat.Models
{
    public class MatchModel
    {
        public int Id { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public UserModel? User1 { get; set; }
        public UserModel? User2 { get; set; }
        public ICollection<MessageModel>? Messages { get; set; }
    }
}
