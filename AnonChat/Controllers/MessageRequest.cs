namespace AnonChat.Controllers
{
    public class MessageRequest
    {
        public int MatchId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
