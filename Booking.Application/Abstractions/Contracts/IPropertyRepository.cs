using Booking.Domain.Estate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IPropertyRepository : IGenericRepository<PropertyEntity>
    {
        //Task<List<PropertyEntity>> GetAllByOwnerIdAsync(Guid guid, CancellationToken cancellationToken); 
        //Task<List<PropertyEntity>> GetAllActiveAsync(CancellationToken cancellationToken);
    }
}
