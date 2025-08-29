using AnonChat.Models.Enum.Category;
using AnonChat.Models.Enum.User;
using AnonChat.Models;

namespace AnonChat.Services.Implementations.Pattern
{
    public class UserUpdateBuilder
    {
        private readonly UserModel _user;

        public UserUpdateBuilder(UserModel user)
        {
            _user = user;
        }

        public UserUpdateBuilder SetState(UserState state)
        {
            _user.State = state;
            return this;
        }

        public UserUpdateBuilder SetSex(CategorySex sex)
        {
            _user.Sex = sex;
            return this;
        }

        public UserUpdateBuilder SetType(CategoryType type)
        {
            _user.Type = type;
            return this;
        }

        public UserUpdateBuilder SetAge(CategoryAge age)
        {
            _user.Age = age;
            return this;
        }

        public UserModel Build() => _user;
    }
}
