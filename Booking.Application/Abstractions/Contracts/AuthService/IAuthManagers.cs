using Booking.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Abstractions.Contracts.AuthService
{
    internal interface IAuthManagers
    {
        string generateToken(UserEntity user);

    }
}
