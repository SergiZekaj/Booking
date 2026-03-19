using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Commands.ChangePassword
{
    public class ChangeUserPasswordCommand : IRequest<Unit>
    {
        public ChangeUserPasswordDto changeUserPasswordDto { get; set; } = new();
    }
}
