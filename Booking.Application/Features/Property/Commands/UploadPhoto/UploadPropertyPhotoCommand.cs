using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Commands.UploadPhoto
{
    public class UploadPropertyPhotoCommand : IRequest<string>
    {
        public Guid PropertyId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
