using Booking.Application.Abstractions.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Booking.Application.Features.Property.Queries.GetById
{
    internal class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdDto>
    {
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<GetPropertyByIdDto> Handle(GetPropertyByIdQuery query, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdWithDetailsAsync(query.Id, cancellationToken);

            if (property == null)
                throw new Exception("Property not found.");

                return new GetPropertyByIdDto
                {
                    Id = property.Id,
                    Name = property.Name,
                    Description = property.Description,
                    PropertyType = property.PropertyType,
                    MaxGuests = property.MaxGuests,
                    CheckInTime = property.CheckInTime,
                    CheckOutTime = property.CheckOutTime,
                    IsActive = property.IsActive,
                    IsApproved = property.IsApproved,
                    CreatedAt = property.CreatedAt,
                    LastModifiedAt = property.LastModifiedAt,
                    LastBookedOnUtc = property.LastBookedOnUtc,
                    OwnerFirstName = property.Owner.FirstName,
                    OwnerLastName = property.Owner.LastName,
                    Country = property.Address.Country,
                    City = property.Address.City,
                    Street = property.Address.Street,
                    PostalCode = property.Address.PostalCode
                };
        }
    }
}


