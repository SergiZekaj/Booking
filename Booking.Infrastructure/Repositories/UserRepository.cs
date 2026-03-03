using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Users;
using Booking.Infrastructure.Persistence;
using Booking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Features
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly BookingDbContext _context;

        public UserRepository(BookingDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
            => await _context.Users
                .Include(u => u.UserRolesEntity)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
