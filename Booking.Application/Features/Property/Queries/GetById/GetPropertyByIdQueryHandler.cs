using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Property.Queries.GetById
{
    internal class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdDto>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<GetPropertyByIdDto> Handle(GetPropertyByIdQuery query, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdWithDetailsAsync(query.Id, cancellationToken);

            if (property == null)
                throw new Exception("Property not found.");

            return _mapper.Map<GetPropertyByIdDto>(property);
        }
    }
}


