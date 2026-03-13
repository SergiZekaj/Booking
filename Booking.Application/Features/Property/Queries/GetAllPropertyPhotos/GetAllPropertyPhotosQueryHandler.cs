using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Property.Queries.GetAllPropertyPhotos
{
    public class GetAllPropertyPhotosQueryHandler : IRequestHandler<GetAllPropertyPhotosQuery, List<GetAllPropertyPhotosDto>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;


        public GetAllPropertyPhotosQueryHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllPropertyPhotosDto>> Handle(GetAllPropertyPhotosQuery query, CancellationToken cancellationToken)
        {
            var allPropertyPhotos = await _propertyImageRepository.GetAllImagesById(query.PropertyId, cancellationToken);
            return _mapper.Map<List<GetAllPropertyPhotosDto>>(allPropertyPhotos);
        }
    }
}
