using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Estate;
using Booking.Domain.OwnerProfiles;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Infrastructure.Repositories
{
    public class OwnerProfileRepository : GenericRepository<OwnerProfileEntity>, IOwnerProfileRepository
    {
        private readonly BookingDbContext _context;

        public OwnerProfileRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OwnerProfileEntity?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => await _context.OwnerProfiles
                .FirstOrDefaultAsync(op => op.UserId == userId, cancellationToken);
    }
}
