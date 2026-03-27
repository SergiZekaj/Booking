using Booking.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace Booking.Application.Features.Bookings.Queries.GetMyBookings
{
    public class GetMyBookingsQuery : IRequest<List<GetMyBookingsDto>>
    {
        public BookingStatus? Status { get; set; }
    }
}

