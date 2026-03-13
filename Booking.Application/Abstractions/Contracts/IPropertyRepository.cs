using Booking.Domain.Estate;
using Booking.Application.Features.Property.Queries.GetAll;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IPropertyRepository : IGenericRepository<PropertyEntity>
    {
        Task<PropertyEntity?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken);
        Task<List<PropertyEntity>> GetAllActiveAsync(PropertyFilterDto filter, CancellationToken cancellationToken);

    }
}