namespace Booking.Application.Abstractions.Contracts
{
    public interface IKafkaProducer
    {
        Task ProduceAsync(string topic, object message);
    }
}