using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Application.Features.Property.Queries.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Commands.Update
{
    internal class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, GetPropertyByIdDto>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetPropertyByIdDto> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdWithDetailsAsync(command.Id, cancellationToken);

            if (property == null)
                throw new Exception("Property not found.");

            if(command.Update.Name != null)
                property.Name = command.Update.Name;

            if(command.Update.Description != null)
                property.Description = command.Update.Description;

            if(command.Update.PropertyType.HasValue)
                property.PropertyType = command.Update.PropertyType.Value;

            if(command.Update.MaxGuests.HasValue)
                property.MaxGuests = command.Update.MaxGuests.Value;

            if(command.Update.CheckInTime.HasValue)
                property.CheckInTime = command.Update.CheckInTime.Value;

            if(command.Update.CheckOutTime.HasValue)
                property.CheckOutTime = command.Update.CheckOutTime.Value;

            property.LastModifiedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetPropertyByIdDto>(property);

        }
    }
}
