using AnonChat.Models;

namespace AnonChat.Services.Interfaces
{
    public interface IUserFactory
    {
        UserModel CreateUser();
    }
}
