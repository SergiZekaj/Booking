using Booking.Domain.Reviews;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IReviewRepository : IGenericRepository<ReviewEntity>
    {
        Task<List<ReviewEntity>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken);
        Task<bool> ExistsByBookingIdAsync(Guid bookingId, CancellationToken cancellationToken);
    }
}
