using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Reviews;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<ReviewEntity>, IReviewRepository
    {
        private readonly BookingDbContext _context;

        public ReviewRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ReviewEntity>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken)
            => await _context.Reviews
                .Include(r => r.Booking)
                .Include(r => r.Guest)
                .Where(r => r.Booking.PropertyId == propertyId)
                .ToListAsync(cancellationToken);

        public async Task<bool> ExistsByBookingIdAsync(Guid bookingId, CancellationToken cancellationToken)
            => await _context.Reviews
                .AnyAsync(r => r.BookingId == bookingId, cancellationToken);
    }
}
