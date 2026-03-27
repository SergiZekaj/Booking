using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Bookings;
using Booking.Domain.Enums;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<BookingEntity>, IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasOverlappingBookingAsync(
            Guid propertyId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AnyAsync(b =>
                    b.PropertyId == propertyId
                    && b.BookingStatus != BookingStatus.Cancelled
                    && b.BookingStatus != BookingStatus.Rejected
                    && b.StartDate < endDate
                    && b.EndDate > startDate,
                    cancellationToken);
        }

        public async Task<List<BookingEntity>> GetMyBookingsAsync(Guid userId, BookingStatus? status, CancellationToken cancellationToken)
        {
            var query = _context.Bookings
                .Where(b => b.GuestId == userId)
                .AsQueryable();

            if (status.HasValue)
                query = query.Where(b => b.BookingStatus == status.Value);

            return await query
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}

