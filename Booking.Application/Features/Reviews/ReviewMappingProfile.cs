using AutoMapper;
using Booking.Application.Features.Reviews.Queries.GetPropertyReviews;
using Booking.Domain.Reviews;

namespace Booking.Application.Features.Reviews
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<ReviewEntity, GetPropertyReviewsDto>()
                .ForMember(dest => dest.GuestFirstName, opt => opt.MapFrom(src => src.Guest.FirstName))
                .ForMember(dest => dest.GuestLastName, opt => opt.MapFrom(src => src.Guest.LastName));
        }
    }
}
