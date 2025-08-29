using AnonChat.Data;
using AnonChat.Models;
using AnonChat.Models.Enum.Category;
using AnonChat.Models.Enum.User;
using AnonChat.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AnonChat.Services.Implementations
{
    public class MatchingService : IMatchingService
    {
        private readonly AnonChatContext _context;
        private readonly UserService _userService;

        public MatchingService(AnonChatContext context)
        {
            _context = context;
            _userService = new UserService(context);
        }

        public async Task<MatchModel?> FindMatchAsync(int userId, CategoryAge age, CategorySex sex, CategoryType type)
        {
            var currentUser = await _context.User.FindAsync(userId);
            if (currentUser == null) return null;

            if (currentUser.State != UserState.InChat)
            {
                await _userService.UpdateUserAsync(userId, builder => builder
                    .SetState(UserState.Waiting)
                    .SetAge(age)
                    .SetSex(sex)
                    .SetType(type));
            }

            var companion = await _context.User
                .Where(u => u.Id != userId &&
                            u.State == UserState.Waiting &&
                            u.Age == age &&
                            u.Sex == sex &&
                            u.Type == type)
                .FirstOrDefaultAsync();

            if (companion != null)
            {
                currentUser.State = UserState.InChat;
                companion.State = UserState.InChat;
                await _context.SaveChangesAsync();

                var match = await FindOrCreateMatchAsync(currentUser.Id, companion.Id, (int)type);
                return match;
            }

            return null;

        }

        public async Task<MatchModel?> FindOrCreateMatchAsync(int user1Id, int user2Id, int categoryId)
        {
            var match = await _context.Matche
                .FirstOrDefaultAsync(m =>
                    !((m.User1Id != user1Id || m.User2Id != user2Id) &&
                      (m.User1Id != user2Id || m.User2Id != user1Id)));

            if (match != null)
                return match;

            match = new MatchModel
            {
                User1Id = user1Id,
                User2Id = user2Id,
                CreatedAt = DateTime.Now
            };

            _context.Matche.Add(match);
            await _context.SaveChangesAsync();

            return match;
        }
    }
}
