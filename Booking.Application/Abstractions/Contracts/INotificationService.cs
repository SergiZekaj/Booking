namespace Booking.Application.Abstractions.Contracts
{
    public interface INotificationService
    {
        Task NotifyUserAsync(string userId, string message);
    }
}