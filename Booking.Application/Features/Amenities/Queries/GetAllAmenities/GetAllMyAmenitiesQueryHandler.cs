using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Amenities.Queries.GetAllAmenities
{
    internal class GetAllMyAmenitiesQueryHandler : IRequestHandler<GetAllMyAmenitiesQuery, List<GetAllMyAmenitiesDto>>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public GetAllMyAmenitiesQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllMyAmenitiesDto>> Handle(GetAllMyAmenitiesQuery query, CancellationToken cancellationToken)
        {
            var allAmenities = await _amenityRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<GetAllMyAmenitiesDto>>(allAmenities);
        }
    }
}