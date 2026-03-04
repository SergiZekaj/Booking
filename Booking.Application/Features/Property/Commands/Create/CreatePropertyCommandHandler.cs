using MediatR;
using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Estate;
using Booking.Domain.Addresses;
using System.Runtime.InteropServices;

namespace Booking.Application.Features.Property.Commands.Create
{
    internal class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Guid>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.PropertyDto;

            var address = new AddressEntity
            {
                Id = Guid.NewGuid(),
                Country = dto.Country,
                City = dto.City,
                Street = dto.Street,
                PostalCode = dto.PostalCode
            };

            var property = new PropertyEntity
            {
                Id = Guid.NewGuid(),
                OwnerId = dto.OwnerId,
                Name = dto.Name,
                Description = dto.Description,
                PropertyType = dto.PropertyType,
                MaxGuests = dto.MaxGuests,
                CheckInTime = dto.CheckInTime,
                CheckOutTime = dto.CheckOutTime,
                Address = address,
                IsActive = true,
                IsApproved = false,
                CreatedAt = DateTime.UtcNow,
            };

            await _propertyRepository.AddAsync(property, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return property.Id;
            
        }
    }
}
