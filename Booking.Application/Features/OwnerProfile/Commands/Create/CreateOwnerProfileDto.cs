using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.OwnerProfile.Commands.Create
{
    public class CreateOwnerProfileDto
    {
        public string? BusinessName { get; set; }
        public string IdentityCardNumber { get; set; } = null!;
    }
}
