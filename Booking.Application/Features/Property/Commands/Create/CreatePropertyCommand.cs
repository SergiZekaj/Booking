using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Commands.Create
{
    public class CreatePropertyCommand : IRequest<Guid>
    {
        public CreatePropertyDto PropertyDto { get; set; } = new();
    }
}
