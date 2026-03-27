using Booking.Domain.Bookings;
using Booking.Domain.Enums;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IBookingRepository : IGenericRepository<BookingEntity>
    {
        Task<bool> HasOverlappingBookingAsync(Guid propertyId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
        Task<List<BookingEntity>> GetMyBookingsAsync(Guid userId, BookingStatus? status, CancellationToken cancellationToken);
    }
}
