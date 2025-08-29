using AnonChat.Data;
using Microsoft.EntityFrameworkCore;

namespace AnonChat.Services.Implementations
{
    public class CleanService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5);

        public CleanService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AnonChatContext>();

                var timeout = DateTime.Now.AddMinutes(-60);
                var inactiveUsers = await context.User
                    .Where(u => u.CreatedAt < timeout)
                    .ToListAsync(stoppingToken);

                if (inactiveUsers.Any())
                {
                    context.User.RemoveRange(inactiveUsers);
                    await context.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
