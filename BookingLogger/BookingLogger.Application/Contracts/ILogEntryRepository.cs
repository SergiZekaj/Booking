using BookingLogger.Domain;

namespace BookingLogger.Application.Contracts
{
    public interface ILogEntryRepository
    {
        Task AddAsync(LogEntry logEntry, CancellationToken cancellationToken);
    }
}