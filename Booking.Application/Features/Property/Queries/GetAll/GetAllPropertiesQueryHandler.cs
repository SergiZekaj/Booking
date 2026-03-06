using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;


namespace Booking.Application.Features.Property.Queries.GetAll
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<GetAllPropertiesDto>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPropertiesDto>> Handle(GetAllPropertiesQuery query, CancellationToken cancellationToken)
        {
            var allProperties = await _propertyRepository.GetAllActiveAsync(query.Filter, cancellationToken);
            return _mapper.Map<List<GetAllPropertiesDto>>(allProperties);

        }    
    }
}
