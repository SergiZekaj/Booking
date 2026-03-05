using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Estate;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Booking.Application.Features.Property.Queries.GetAll;

namespace Booking.Infrastructure.Repositories
{
    public class PropertyRepository : GenericRepository<PropertyEntity>, IPropertyRepository
    {
        private readonly BookingDbContext _context;

        public PropertyRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PropertyEntity?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken)
            => await _context.Properties
            .Include(p => p.Owner)
            .Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<List<PropertyEntity>> GetAllActiveAsync(PropertyFilterDto filter, CancellationToken cancellationToken)
        { 
            var query = _context.Properties
                .Include(p => p.Address)
                .Where(p => p.IsActive && p.IsApproved)
                .AsQueryable();

            if (filter.City != null)
                query = query.Where(p => p.Address.City == filter.City);

            if (filter.Country != null)
                query = query.Where(p => p.Address.Country == filter.Country);

            if (filter.PropertyType != null)
                query = query.Where(p => p.PropertyType == filter.PropertyType);

            if (filter.MaxGuests.HasValue && filter.MaxGuests >= 0)
                query = query.Where(p => p.MaxGuests >= filter.MaxGuests);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
