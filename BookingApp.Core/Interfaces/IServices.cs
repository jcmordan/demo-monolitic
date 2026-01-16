using BookingApp.Core.Dtos;

namespace BookingApp.Core.Interfaces;

public interface IUserService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int id);
}

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    Task<RoomDto?> GetRoomByIdAsync(int id);
    Task<RoomDto> CreateRoomAsync(CreateRoomDto createRoomDto);
    Task<RoomDto?> UpdateRoomAsync(int id, UpdateRoomDto updateRoomDto);
    Task<bool> DeleteRoomAsync(int id);
}

public interface IBookingService
{
    Task<ServiceResult<BookingDto>> CreateBookingAsync(int userId, CreateBookingDto createBookingDto);
    Task<bool> CancelBookingAsync(int bookingId, int userId);
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
}

public interface INotificationService
{
    Task NotifyAsync(int userId, string message);
}

public interface IPaymentService
{
    Task<ServiceResult<PaymentDto>> AddPaymentAsync(AddPaymentDto addPaymentDto);
    Task<IEnumerable<PaymentDto>> GetPaymentsAsync(string? status);
}
