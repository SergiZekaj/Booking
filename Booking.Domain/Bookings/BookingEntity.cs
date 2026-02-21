using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Booking.Domain.Enums;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Booking.Domain.Reviews;
using Booking.Domain.Users;
using Booking.Domain.Estate;

namespace Booking.Domain.Bookings
{
    public class BookingEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Property))]
        public Guid PropertyId { get; set; }
        public PropertyEntity Property { get; set; } = null!;

        [ForeignKey(nameof(Guest))]
        public Guid GuestId { get; set; }  
        public required UserEntity Guest { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }
        public decimal CleaningFee { get; set; }
        public decimal AmenitiesUpCharge { get; set; }
        public decimal PriceForPeriod { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? ConfirmedOnUtc { get; set; } 
        public DateTime? RejectedOnUtc {  get; set; } 
        public DateTime? CompletedOnUtc {  get; set; }
        public DateTime? CancelledOnUtc {  get; set; }
        
        public ReviewEntity? Review {  get; set; }
    }
}
