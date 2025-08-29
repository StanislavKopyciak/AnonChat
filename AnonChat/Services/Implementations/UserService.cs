using AnonChat.Data;
using AnonChat.Models;
using AnonChat.Services.Implementations.Pattern;
using AnonChat.Services.Interface;
using AnonChat.Services.Interfaces;

namespace AnonChat.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AnonChatContext _anonChatContext;
        private readonly IUserFactory userFactory;

        public UserService(AnonChatContext anonChatContext)
        {
            _anonChatContext = anonChatContext;
            userFactory = new NormalUserFactory();
        }

        public async Task<UserModel> CreateUserAsync()
        {
            var user = userFactory.CreateUser();
            _anonChatContext.User.Add(user);
            await _anonChatContext.SaveChangesAsync();

            return user;
        }


        public async Task<UserModel> GetUserAsync(int id) 
        {
            var user = await _anonChatContext.User.FindAsync(id);

            if (user == null)
                throw new Exception("User not found");

            return user;
        }


        public async Task UpdateUserAsync(int id, Action<UserUpdateBuilder> buildAction)
        {
            var user = await _anonChatContext.User.FindAsync(id);

            if (user == null)
                throw new Exception("User not found");

            var builder = new UserUpdateBuilder(user);
            buildAction(builder);

            await _anonChatContext.SaveChangesAsync();
        }
    }
}
