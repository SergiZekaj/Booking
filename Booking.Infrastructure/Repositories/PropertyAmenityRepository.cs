using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Amenities;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class PropertyAmenityRepository : GenericRepository<PropertyAmenityEntity>, IPropertyAmenityRepository
    {
        private readonly BookingDbContext _context;

        public PropertyAmenityRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PropertyAmenityEntity?> GetByPropertyAndAmenityAsync(Guid propertyId, Guid amenityId, CancellationToken cancellationToken)
            => await _context.PropertyAmenities
                .FirstOrDefaultAsync(pa => pa.PropertyId == propertyId && pa.AmenityId == amenityId, cancellationToken);

        public async Task<List<PropertyAmenityEntity>> GetAllByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken)
            => await _context.PropertyAmenities
                .Include(pa => pa.Amenity)
                .Where(pa => pa.PropertyId == propertyId)
                .ToListAsync(cancellationToken);

        public async Task<List<AmenityEntity>> GetAmenitiesByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken)
            => await _context.PropertyAmenities
                .Where(pa => pa.PropertyId == propertyId)
                .Select(pa => pa.Amenity)
                .ToListAsync(cancellationToken);
    }
}