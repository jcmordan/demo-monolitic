using BookingApp.Core.Interfaces;

namespace BookingApp.Core.Services;

public class NotificationService : INotificationService
{
    // Keeping it simple for the example, just logging or printing
    public Task NotifyAsync(int userId, string message)
    {
        Console.WriteLine($"[NOTIFICATION to User {userId}]: {message}");
        return Task.CompletedTask;
    }
}
