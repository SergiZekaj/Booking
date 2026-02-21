using Booking.Domain.Roles;
using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.UserRoles
{
    public class UserRoleEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        public Guid RoleId { get; set; }
        public RoleEntity Role { get; set; } = null!;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
