using Booking.Application.Abstractions.Contracts;
using Booking.Domain.PropertyImage;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImageEntity>, IPropertyImageRepository
    {
        private readonly BookingDbContext _context;
        public PropertyImageRepository(BookingDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<PropertyImageEntity>> GetAllImagesById(Guid id, CancellationToken cancellationToken)
            => await _context.PropertyImage
            .Where(p => p.PropertyId == id)
            .ToListAsync(cancellationToken);
    }
}
