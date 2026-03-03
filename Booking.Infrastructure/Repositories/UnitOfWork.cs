using Booking.Application.Contracts;
using Booking.Infrastructure.Persistence;

namespace Booking.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookingDbContext _context;

    public UnitOfWork(BookingDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}