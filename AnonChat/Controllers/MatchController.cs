using AnonChat.Data;
using AnonChat.Models;
using AnonChat.Models.Enum.Category;
using AnonChat.Models.Enum.User;
using AnonChat.Services.Implementations;
using AnonChat.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnonChat.Controllers
{
    public class MatchController : Controller
    {
        private readonly MatchingService _matchingService;
        private readonly IUserService _userService;
        private readonly AnonChatContext _context;

        public MatchController(MatchingService matchingService, IUserService userService, AnonChatContext context)
        {
            _matchingService = matchingService;
            _userService = userService;
            _context = context;
        }

        public async Task<IActionResult> Index(int? matchId = null)
        {
            if (matchId.HasValue)
            {
                ViewBag.MatchId = matchId.Value;
            }
            else
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (!userId.HasValue)
                {
                    UserModel user = await _userService.CreateUserAsync();
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("State", user.State.ToString());
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindMatch([FromBody] FindMatchRequest request)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (!currentUserId.HasValue)
                return Json(new { success = false });

            var category = (CategoryType)int.Parse(request.Category);
            var sex = (CategorySex)int.Parse(request.Sex);
            var age = (CategoryAge)int.Parse(request.Age);

            var currentUser = await _userService.GetUserAsync(currentUserId.Value);
            if (currentUser.State == UserState.InChat)
            {
                var match = await _context.Matche
                    .Where(m => m.User1Id == currentUserId.Value || m.User2Id == currentUserId.Value)
                    .OrderByDescending(m => m.CreatedAt)
                    .FirstOrDefaultAsync();

                if (match != null)
                    return Json(new { success = true, matchId = match.Id });
            }

            var matchModel = await _matchingService.FindMatchAsync(
                currentUserId.Value,
                age,
                sex,
                category
            );

            if (matchModel == null)
                return Json(new { success = false });

            return Json(new { success = true, matchId = matchModel.Id });
        }

        [HttpPost]
        public async Task<IActionResult> CancelSearch()
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (!currentUserId.HasValue)
                return Json(new { success = false });


            await _userService.UpdateUserAsync(currentUserId.Value, builder => builder
                .SetState(UserState.Offline)
            );
            

            return Json(new { success = true });
        }
    }
}