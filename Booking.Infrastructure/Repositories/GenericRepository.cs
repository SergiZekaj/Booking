using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Users;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where  TEntity : class
    {
        private readonly BookingDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BookingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
            => await _dbSet.ToListAsync(cancellationToken);

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
            => await _dbSet.AddAsync(entity, cancellationToken);

        public void Update(TEntity entity)
            => _dbSet.Update(entity);

        public void Delete(TEntity entity)
            => _dbSet.Remove(entity);
    }
}
