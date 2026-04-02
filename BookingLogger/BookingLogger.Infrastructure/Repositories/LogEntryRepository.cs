using BookingLogger.Application.Contracts;
using BookingLogger.Domain;
using BookingLogger.Infrastructure.Persistence;

namespace BookingLogger.Infrastructure.Repositories
{
    public class LogEntryRepository : ILogEntryRepository
    {
        private readonly LoggerDbContext _context;

        public LogEntryRepository(LoggerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LogEntry logEntry, CancellationToken cancellationToken)
        {
            await _context.LogEntries.AddAsync(logEntry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}