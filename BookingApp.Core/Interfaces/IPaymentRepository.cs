using BookingApp.Core.Entities;

namespace BookingApp.Core.Interfaces;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(int id);
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<IEnumerable<Payment>> GetByStatusAsync(string status);
    Task AddAsync(Payment payment);
    Task SaveChangesAsync();
}
