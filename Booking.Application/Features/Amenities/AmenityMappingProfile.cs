using AutoMapper;
using Booking.Application.Features.Amenities.Queries.GetAllAmenities;
using Booking.Application.Features.Amenities.Queries.GetPropertyAmenities;
using Booking.Domain.Amenities;

namespace Booking.Application.Features.Amenities
{
    public class AmenityMappingProfile : Profile
    {
        public AmenityMappingProfile()
        {
            CreateMap<AmenityEntity, GetAllMyAmenitiesDto>();
            CreateMap<AmenityEntity, GetPropertyAmenitiesDto>();
        }
    }
}