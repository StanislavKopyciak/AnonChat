using AnonChat.Models;
using AnonChat.Models.Enum.Category;

namespace AnonChat.Services.Interface
{
    public interface IMatchingService
    {
        Task<MatchModel?> FindMatchAsync(int userId, CategoryAge age, CategorySex sex, CategoryType type);
    }
}
