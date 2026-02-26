using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Users;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Features
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingDbContext _context;

        public UserRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByEmailAsync(string email)
            => await _context.Users
                .Include(u => u.UserRolesEntity)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<UserEntity?> GetByIdAsync(Guid id)
            => await _context.Users.FindAsync(id);

        public async Task<List<UserEntity>> GetAllAsync()
            => await _context.Users.ToListAsync();

        public async Task AddAsync(UserEntity entity)
            => await _context.Users.AddAsync(entity);

        public void Update(UserEntity entity)
            => _context.Users.Update(entity);

        public void Delete(UserEntity entity)
            => _context.Users.Remove(entity);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
