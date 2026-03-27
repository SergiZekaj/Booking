using System;

namespace Booking.Application.Features.Bookings.Commands.Create
{
    public class CreateBookingDto
    {
        public Guid PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }
        public decimal PricePerNight { get; set; }
    }
}

