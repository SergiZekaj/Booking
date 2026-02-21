using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.Users
{
    internal class CreateUserDto
    {
        public string FirstName { get; init; }
        public string LaseName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
