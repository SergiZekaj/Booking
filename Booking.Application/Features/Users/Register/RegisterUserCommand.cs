using MediatR;
using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public CreateUserDto UserDto { get; set; }
    }
}
