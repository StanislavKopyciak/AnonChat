using AnonChat.Models;
using AnonChat.Models.Enum.User;
using AnonChat.Services.Interfaces;
using AnonChat.Models.Enum.Category;

namespace AnonChat.Services.Implementations.Pattern
{
    public class NormalUserFactory : IUserFactory
    {
        public UserModel CreateUser()
        {
            return new UserModel
            {
                Type = CategoryType.Спілкування,
                Sex = CategorySex.Чоловік,      
                Age = CategoryAge.Від18до25,         
                State = UserState.Offline,
                CreatedAt = DateTime.Now
            };
        }
    }
}
