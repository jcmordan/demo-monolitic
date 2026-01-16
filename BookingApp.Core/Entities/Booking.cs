namespace BookingApp.Core.Entities;

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public bool IsCancelled { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
