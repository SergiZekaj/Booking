using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Features.Property.Commands.Delete
{
    internal class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyRepository _propertyRepository;
        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork) 
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePropertyCommand command, CancellationToken cancellationToken)
        { 
            var property = await _propertyRepository.GetByIdAsync(command.Id, cancellationToken);
            if (property == null)
                throw new Exception("Property not found.");

            _propertyRepository.Delete(property);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
