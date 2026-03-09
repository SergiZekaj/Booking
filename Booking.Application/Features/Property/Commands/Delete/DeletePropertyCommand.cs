using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Commands.Delete
{
    public class DeletePropertyCommand : IRequest<Unit>
    {
        public Guid Id {  get; set; }
    }
}
