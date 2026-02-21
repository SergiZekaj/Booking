using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Users.Register
{
    internal class RegisterUserCommandHandler(IUserRepository user) : IRequestHandler<RegisterUserCommand, Guid>
    {
        public Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
