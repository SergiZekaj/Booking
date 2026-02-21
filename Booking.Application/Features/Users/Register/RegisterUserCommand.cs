using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public CreateUserDto { get; set; }
    }
}
