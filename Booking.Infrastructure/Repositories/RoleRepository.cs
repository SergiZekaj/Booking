using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Roles;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<RoleEntity>, IRoleRepository
    {
        private readonly BookingDbContext _context;

        public RoleRepository(BookingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RoleEntity?> GetDefaultRoleAsync(CancellationToken cancellationToken)
            => await _context.Roles.FirstOrDefaultAsync(r => r.IsDefault, cancellationToken);
    }
}
