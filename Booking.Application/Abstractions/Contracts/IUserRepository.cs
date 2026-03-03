using Booking.Domain.Users;
using System;
using System.Collections.Generic;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmailAsync(string email);
    }
}
