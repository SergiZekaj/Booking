using Booking.Application.Abstractions.Contracts;
using Booking.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Booking.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyUserAsync(string userId, string message)
        {
            await _hubContext.Clients.Group(userId).SendAsync("ReceiveNotification", message);
        }
    }
}