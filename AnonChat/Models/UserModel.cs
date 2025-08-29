using AnonChat.Models.Enum.Category;
using AnonChat.Models.Enum.User;

namespace AnonChat.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public CategoryType Type { get; set; }
        public CategorySex Sex { get; set; }
        public CategoryAge Age { get; set; }
        public UserState State { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<MessageModel>? Messages { get; set; }
        public ICollection<MatchModel>? Matches { get; set; }
    }
}
