using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Estate;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Infrastructure.Repositories
{
    public class PropertyRepository : GenericRepository<PropertyEntity>, IPropertyRepository
    {
        private readonly BookingDbContext _context;

        public PropertyRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<PropertyRepository?> GetAllByOwnerIdAsync(Guid? ownerId, CancellationToken cancellationToken)
        //    => await _context.Properties
        //    .Include(p => p.)
    }
}
