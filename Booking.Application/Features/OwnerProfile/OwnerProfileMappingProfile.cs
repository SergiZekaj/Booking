using AutoMapper;
using Booking.Application.Features.OwnerProfile.Queries.GetMyOwnerProfile;
using Booking.Domain.OwnerProfiles;

namespace Booking.Application.Features.OwnerProfile
{
    public class OwnerProfileMappingProfile : Profile
    {
        public OwnerProfileMappingProfile()
        {
            CreateMap<OwnerProfileEntity, GetMyOwnerProfileDto>();
        }
    }
}