using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Amenities.Command.RemoveAmenityFromProperty
{
    internal class RemoveAmenityFromPropertyCommandHandler : IRequestHandler<RemoveAmenityFromPropertyCommand, Unit>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyAmenityRepository _propertyAmenityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAmenityFromPropertyCommandHandler(IHttpContextAccessor httpContextAccessor, IPropertyRepository propertyRepository, IPropertyAmenityRepository propertyAmenityRepository, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _propertyRepository = propertyRepository;
            _propertyAmenityRepository = propertyAmenityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveAmenityFromPropertyCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);

            var property = await _propertyRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null)
                throw new Exception("Property not found.");
            if (property.OwnerId != userId)
                throw new Exception("You are not authorized.");

            var propertyAmenity = await _propertyAmenityRepository.GetByPropertyAndAmenityAsync(command.PropertyId, command.AmenityId, cancellationToken);
            if (propertyAmenity == null)
                throw new Exception("Amenity not found on this property.");

            _propertyAmenityRepository.Delete(propertyAmenity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}