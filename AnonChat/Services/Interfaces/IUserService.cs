using AnonChat.Models;
using AnonChat.Models.Enum.User;
using AnonChat.Services.Implementations.Pattern;

namespace AnonChat.Services.Interface
{
    public interface IUserService
    {
        Task<UserModel> CreateUserAsync();

        Task<UserModel> GetUserAsync(int id);

        Task UpdateUserAsync(int id, Action<UserUpdateBuilder> buildAction);
    }
}
