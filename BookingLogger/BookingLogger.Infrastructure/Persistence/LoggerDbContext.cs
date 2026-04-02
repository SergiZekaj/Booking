using BookingLogger.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookingLogger.Infrastructure.Persistence
{
    public class LoggerDbContext : DbContext
    {
        public LoggerDbContext(DbContextOptions<LoggerDbContext> options) : base(options) { }
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
