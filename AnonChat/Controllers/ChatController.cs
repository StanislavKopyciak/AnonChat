using AnonChat.Data;
using AnonChat.Models.Enum.User;
using AnonChat.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace AnonChat.Controllers
{
    public class ChatController : Controller
    {
        private readonly AnonChatContext _context;
        private readonly UserService _userService;
        private readonly MessageService _messageService;
        private readonly ChatStatusService _chatStatusService;

        public ChatController(AnonChatContext context, UserService userService, MessageService messageService, ChatStatusService chatStatusService)
        {
            _context = context;
            _userService = userService;
            _messageService = messageService;
            _chatStatusService = chatStatusService;
        }

        public IActionResult Index(int matchId)
        {
            ViewBag.MatchId = matchId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageRequest request)
        {
            var senderId = HttpContext.Session.GetInt32("UserId");
            if (!senderId.HasValue) return Json(new { success = false });

            await _messageService.SendMessageAsync(senderId.Value, request.MatchId, request.Text);

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(int matchId)
        {
            var messages = await _messageService.GetMessagesAsync(matchId);
            return Json(new { success = true, messages });
        }

        [HttpPost]
        public async Task<IActionResult> CancelChat(int matchId)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (!currentUserId.HasValue)
                return Json(new { success = false });

            await _userService.UpdateUserAsync(currentUserId.Value, b => b.SetState(UserState.Offline));

            await _messageService.DeleteMatchAsync(matchId);

            return Json(new { success = true, redirectUrl = Url.Action("Index", "Match") });
        }

        [HttpGet]
        public async Task<IActionResult> CheckCompanion(int matchId)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (!currentUserId.HasValue)
                return Json(new { success = false });

            bool isOnline = _chatStatusService.IsCompanionOnline(matchId, currentUserId.Value);

            if (!isOnline)
            {
                await _userService.UpdateUserAsync(currentUserId.Value, b => b.SetState(UserState.Offline));
                return Json(new { success = false, redirectUrl = Url.Action("Index", "Match") });
            }

            return Json(new { success = true });
        }
    }
}