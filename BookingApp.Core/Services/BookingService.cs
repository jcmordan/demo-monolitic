using BookingApp.Core.Dtos;
using BookingApp.Core.Entities;
using BookingApp.Core.Interfaces;

namespace BookingApp.Core.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly INotificationService _notificationService;

    public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository, INotificationService notificationService)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _notificationService = notificationService;
    }

    public async Task<ServiceResult<BookingDto>> CreateBookingAsync(int userId, CreateBookingDto createBookingDto)
    {
        var room = await _roomRepository.GetByIdAsync(createBookingDto.RoomId);
        if (room == null) return ServiceResult<BookingDto>.Failure("Room not found.");
        if (!room.IsAvailable) return ServiceResult<BookingDto>.Failure("Room is not available.");

        var isOverlapping = await _bookingRepository.AnyOverlappingAsync(
            createBookingDto.RoomId, 
            createBookingDto.CheckInDate, 
            createBookingDto.CheckOutDate);
            
        if (isOverlapping) return ServiceResult<BookingDto>.Failure("Room is already booked for the selected dates.");

        var booking = new Booking
        {
            RoomId = createBookingDto.RoomId,
            UserId = userId,
            CheckInDate = createBookingDto.CheckInDate,
            CheckOutDate = createBookingDto.CheckOutDate,
            IsCancelled = false
        };

        await _bookingRepository.AddAsync(booking);
        
        // Notify user
        await _notificationService.NotifyAsync(userId, $"Your booking for room {room.Name} has been confirmed.");

        return ServiceResult<BookingDto>.SuccessResult(new BookingDto(booking.Id, booking.RoomId, booking.UserId, booking.CheckInDate, booking.CheckOutDate, booking.IsCancelled));
    }

    public async Task<bool> CancelBookingAsync(int bookingId, int userId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null || booking.UserId != userId || booking.IsCancelled) return false;

        booking.IsCancelled = true;
        await _bookingRepository.UpdateAsync(booking);

        await _notificationService.NotifyAsync(userId, $"Your booking {bookingId} has been cancelled.");

        return true;
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Select(b => new BookingDto(b.Id, b.RoomId, b.UserId, b.CheckInDate, b.CheckOutDate, b.IsCancelled));
    }
}
