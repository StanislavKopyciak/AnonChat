using AnonChat.Models.Enum.Category;

namespace AnonChat.Models
{
    public class CategoryModel
    {

        public int Id { get; set; }
        public CategoryType Type { get; set; }
        public CategorySex Sex { get; set; }
        public CategoryAge Age { get; set; }

        public ICollection<MatchModel>? Matches { get; set; }
    }
}
