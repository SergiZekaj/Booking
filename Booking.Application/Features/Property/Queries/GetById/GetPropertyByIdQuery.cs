using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Queries.GetById
{
    public class GetPropertyByIdQuery : IRequest<GetPropertyByIdDto>
    {
        public Guid Id { get; set; }
    }
}
