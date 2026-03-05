using Booking.Application.Features.Property.Queries.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Queries.GetAll
{
    public class GetAllPropertiesQuery : IRequest<List<GetAllPropertiesDto>>
    {
        public PropertyFilterDto Filter { get; set; } = new();
    }
}
