using Microsoft.AspNetCore.Http;

namespace Booking.Application.Abstractions.Contracts
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
