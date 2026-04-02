using BookingLogger.Application.Contracts;
using BookingLogger.Domain;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookingLogger.Infrastructure.Consumers
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(
            IServiceScopeFactory scopeFactory,
            IConfiguration configuration,
            ILogger<KafkaConsumerService> logger)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = "booking-logger-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(new[]
            {
                "booking-created",
                "booking-confirmed",
                "booking-cancelled",
                "booking-rejected",
                "user-registered",
                "error-occurred"
            });

            while (!cancellationToken.IsCancellationRequested) 
            {
                try
                {
                    var result = consumer.Consume(cancellationToken); 

                    using var scope = _scopeFactory.CreateScope();
                    var repo = scope.ServiceProvider.GetRequiredService<ILogEntryRepository>();

                    var logEntry = new LogEntry
                    {
                        Id = Guid.NewGuid(),
                        EventType = result.Topic,
                        Payload = result.Message.Value,
                        IsError = result.Topic == "error-occurred",
                        Timestamp = DateTime.UtcNow,
                        Source = "Booking.Api"
                    };

                    await repo.AddAsync(logEntry, cancellationToken);
                    _logger.LogInformation("Logged event: {EventType}", result.Topic);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error consuming Kafka message");
                }
            }
        }
    }
}