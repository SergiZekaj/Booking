using Booking.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IRoleRepository : IGenericRepository<RoleEntity>
    {
        Task<RoleEntity?> GetDefaultRoleAsync(CancellationToken cancellationToken);
    }
}
