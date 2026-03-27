using System;
using Booking.Domain.Enums;

namespace Booking.Application.Features.Bookings.Queries.GetMyBookings
{
    public class GetMyBookingsDto
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public decimal PriceForPeriod { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedOnUtc { get; set; }
        public DateTime? RejectedOnUtc { get; set; }
        public DateTime? CancelledOnUtc { get; set; }
        public DateTime? CompletedOnUtc { get; set; }
    }
}

