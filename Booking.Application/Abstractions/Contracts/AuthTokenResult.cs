namespace Booking.Application.Abstractions.Contracts;

public record AuthTokenResult(string Token, string TokenType, DateTime ExpiresAt);
