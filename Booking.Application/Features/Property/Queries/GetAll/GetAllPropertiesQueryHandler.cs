using Booking.Application.Abstractions.Contracts;
using MediatR;


namespace Booking.Application.Features.Property.Queries.GetAll
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<GetAllPropertiesDto>>
    {
        private readonly IPropertyRepository _propertyRepository;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<List<GetAllPropertiesDto>> Handle(GetAllPropertiesQuery query, CancellationToken cancellationToken)
        {
            var allProperties = await _propertyRepository.GetAllActiveAsync(query.Filter, cancellationToken);

            return allProperties.Select(property => new GetAllPropertiesDto
            {
                Id = property.Id,
                Name = property.Name,
                Description = property.Description,
                PropertyType = property.PropertyType,
                MaxGuests = property.MaxGuests,
                IsApproved = property.IsApproved,
                CheckInTime = property.CheckInTime,
                CheckOutTime = property.CheckOutTime,
                Country = property.Address.Country,
                City = property.Address.City,
                
            }).ToList();
        }    
    }
}
