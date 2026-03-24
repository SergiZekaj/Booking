using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Amenities.Queries.GetPropertyAmenities
{
    internal class GetPropertyAmenitiesQueryHandler : IRequestHandler<GetPropertyAmenitiesQuery, List<GetPropertyAmenitiesDto>>
    {
        private readonly IPropertyAmenityRepository _propertyAmenityRepository;
        private readonly IMapper _mapper;

        public GetPropertyAmenitiesQueryHandler(IPropertyAmenityRepository propertyAmenityRepository, IMapper mapper)
        {
            _propertyAmenityRepository = propertyAmenityRepository;
            _mapper = mapper;
        }

        public async Task<List<GetPropertyAmenitiesDto>> Handle(GetPropertyAmenitiesQuery query, CancellationToken cancellationToken)
        {
            var amenities = await _propertyAmenityRepository.GetAmenitiesByPropertyIdAsync(query.PropertyId, cancellationToken);
            return _mapper.Map<List<GetPropertyAmenitiesDto>>(amenities);
        }
    }
}
