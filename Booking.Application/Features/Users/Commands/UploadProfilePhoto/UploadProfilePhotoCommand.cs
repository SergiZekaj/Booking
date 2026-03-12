using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Users.Commands.UploadProfilePhoto
{
    public class UploadProfilePhotoCommand : IRequest<string>
    {
        public IFormFile File { get; set; } = null!;
    }
}
