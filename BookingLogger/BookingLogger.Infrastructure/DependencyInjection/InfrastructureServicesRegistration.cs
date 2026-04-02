using BookingLogger.Application.Contracts;
using BookingLogger.Infrastructure.Consumers;
using BookingLogger.Infrastructure.Persistence;
using BookingLogger.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingLogger.Infrastructure.DependencyInjection
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<LoggerDbContext>(options => 
                options.UseSqlServer(connectionString)); 

            services.AddScoped<ILogEntryRepository, LogEntryRepository>();
            services.AddHostedService<KafkaConsumerService>();

            return services;
        }
    }
}