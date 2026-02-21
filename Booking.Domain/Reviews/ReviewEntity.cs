using Booking.Domain.Bookings;
using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Domain.Reviews
{
    public class ReviewEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Booking))]
        public Guid BookingId { get; set; }
        public BookingEntity Booking { get; set; } = null!;
        [ForeignKey(nameof(Guest))]
        public Guid GuestId { get; set; }
        public UserEntity Guest { get; set; } = null!;
        public decimal Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
