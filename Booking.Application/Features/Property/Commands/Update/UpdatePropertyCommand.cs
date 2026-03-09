using Booking.Application.Features.Property.Queries.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Commands.Update
{
    public class UpdatePropertyCommand : IRequest<GetPropertyByIdDto>
    {
        public Guid Id { get; set; }
        public UpdatePropertyDto Update { get; set; } = new();
    }
}
