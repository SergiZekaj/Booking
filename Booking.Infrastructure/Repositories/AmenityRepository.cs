using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Amenities;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class AmenityRepository : GenericRepository<AmenityEntity>, IAmenityRepository
    {
        private readonly BookingDbContext _context;

        public AmenityRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken)
            => await _context.Amenities.AnyAsync(a => a.Name == name, cancellationToken);
    }
}