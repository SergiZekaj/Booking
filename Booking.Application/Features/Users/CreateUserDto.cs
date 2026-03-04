using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users
{
    public class CreateUserDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
