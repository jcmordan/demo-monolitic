namespace BookingApp.Core.Dtos;

public record RoomDto(int Id, string Name, string Description, decimal PricePerNight, bool IsAvailable);
public record CreateRoomDto(string Name, string Description, decimal PricePerNight);
public record UpdateRoomDto(string Name, string Description, decimal PricePerNight, bool IsAvailable);

public record BookingDto(int Id, int RoomId, int UserId, DateTime CheckInDate, DateTime CheckOutDate, bool IsCancelled);
public record CreateBookingDto(int RoomId, DateTime CheckInDate, DateTime CheckOutDate);
