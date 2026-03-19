using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Commands.ChangePassword
{
    public class ChangeUserPasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}