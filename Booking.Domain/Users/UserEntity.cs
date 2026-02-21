using Booking.Domain.Bookings;
using Booking.Domain.Estate;
using Booking.Domain.OwnerProfiles;
using Booking.Domain.Reviews;
using Booking.Domain.UserRoles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking.Domain.Users
{
    public class UserEntity //changed internal to public so the other projects can access it.
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = null!; //new
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? LastModifiedAt { get; set; }

        public OwnerProfileEntity? OwnerProfileEntity { get; set; }
        public ICollection<PropertyEntity> OwnedProperties { get; set; } = new List<PropertyEntity>();
        public ICollection<BookingEntity> GuestBookings { get; set; } = new List<BookingEntity>();
        public ICollection<UserRoleEntity> UserRolesEntity { get; set; } = new List<UserRoleEntity>();
        public ICollection<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();

    }
}
