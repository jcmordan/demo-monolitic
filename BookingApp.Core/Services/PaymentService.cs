using BookingApp.Core.Dtos;
using BookingApp.Core.Entities;
using BookingApp.Core.Interfaces;

namespace BookingApp.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBookingRepository _bookingRepository;

    public PaymentService(IPaymentRepository paymentRepository, IBookingRepository bookingRepository)
    {
        _paymentRepository = paymentRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<ServiceResult<PaymentDto>> AddPaymentAsync(AddPaymentDto addPaymentDto)
    {
        var booking = await _bookingRepository.GetByIdAsync(addPaymentDto.BookingId);
        if (booking == null) return ServiceResult<PaymentDto>.Failure("Booking not found.");

        var payment = new Payment
        {
            BookingId = addPaymentDto.BookingId,
            Amount = addPaymentDto.Amount,
            PaymentDate = DateTime.UtcNow,
            Status = "Completed" // For demo purposes, we'll mark it as completed immediately
        };

        await _paymentRepository.AddAsync(payment);
        await _paymentRepository.SaveChangesAsync();

        return ServiceResult<PaymentDto>.SuccessResult(new PaymentDto(
            payment.Id,
            payment.BookingId,
            payment.Amount,
            payment.PaymentDate,
            payment.Status));
    }

    public async Task<IEnumerable<PaymentDto>> GetPaymentsAsync(string? status)
    {
        IEnumerable<Payment> payments;
        
        if (string.IsNullOrEmpty(status))
        {
            payments = await _paymentRepository.GetAllAsync();
        }
        else
        {
            payments = await _paymentRepository.GetByStatusAsync(status);
        }

        return payments.Select(p => new PaymentDto(
            p.Id,
            p.BookingId,
            p.Amount,
            p.PaymentDate,
            p.Status));
    }
}
