using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Addresses;
using Booking.Domain.Estate;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Property.Commands.Create
{
    internal class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var dto = request.PropertyDto;

            if (await _propertyRepository.ExistsAsync(userId, dto.Name, cancellationToken))
                throw new Exception("You already have a property with this name.");

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
                OwnerId = userId,
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
