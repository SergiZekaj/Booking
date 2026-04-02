using System.ComponentModel.DataAnnotations;

namespace BookingLogger.Domain
{
    public class LogEntry
    {
        [Key]
        public Guid Id { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public bool IsError { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Source { get; set; }
    }
}