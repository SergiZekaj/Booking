using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Amenities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Booking.Application.Features.Amenities.Command.AddAmenityToProperty
{
    internal class AddAmenityToPropertyCommandHandler : IRequestHandler<AddAmenityToPropertyCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAmenityRepository _amenityRepository;
        private readonly IPropertyAmenityRepository _propertyAmenityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddAmenityToPropertyCommandHandler(IHttpContextAccessor httpContextAccessor, IPropertyRepository propertyRepository, IAmenityRepository amenityRepository, IPropertyAmenityRepository propertyAmenityRepository, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _propertyRepository = propertyRepository;
            _amenityRepository = amenityRepository;
            _propertyAmenityRepository = propertyAmenityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddAmenityToPropertyCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);

            var property = await _propertyRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null)
                throw new Exception("Property not found.");
            if (property.OwnerId != userId)
                throw new Exception("You are not authorized.");

            var amenity = await _amenityRepository.GetByIdAsync(command.AmenityId, cancellationToken);
            if (amenity == null)
                throw new Exception("Amenity not found.");

            var existing = await _propertyAmenityRepository.GetByPropertyAndAmenityAsync(command.PropertyId, command.AmenityId, cancellationToken);
            if (existing != null)
                throw new Exception("Amenity already added to this property.");

            var propertyAmenity = new PropertyAmenityEntity
            {
                PropertyId = command.PropertyId,
                AmenityId = command.AmenityId,
            };


            await _propertyAmenityRepository.AddAsync(propertyAmenity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}