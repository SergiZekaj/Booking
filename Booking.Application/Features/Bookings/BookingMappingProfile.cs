using AutoMapper;
using Booking.Application.Features.Bookings.Queries.GetMyBookings;
using Booking.Domain.Bookings;

namespace Booking.Application.Features.Bookings
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<BookingEntity, GetMyBookingsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.PropertyId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.GuestCount, opt => opt.MapFrom(src => src.GuestCount))
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.BookingStatus))
                .ForMember(dest => dest.PriceForPeriod, opt => opt.MapFrom(src => src.PriceForPeriod))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ConfirmedOnUtc, opt => opt.MapFrom(src => src.ConfirmedOnUtc))
                .ForMember(dest => dest.RejectedOnUtc, opt => opt.MapFrom(src => src.RejectedOnUtc))
                .ForMember(dest => dest.CancelledOnUtc, opt => opt.MapFrom(src => src.CancelledOnUtc))
                .ForMember(dest => dest.CompletedOnUtc, opt => opt.MapFrom(src => src.CompletedOnUtc));
        }
    }
}

