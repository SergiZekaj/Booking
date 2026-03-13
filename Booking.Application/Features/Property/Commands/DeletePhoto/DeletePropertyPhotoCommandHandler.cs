using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Property.Commands.DeletePhoto
{
    internal class DeletePropertyPhotoCommandHandler : IRequestHandler<DeletePropertyPhotoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropertyRepository _propertyRepository;

        public DeletePropertyPhotoCommandHandler(IPropertyImageRepository propertyImageRepository, IUnitOfWork unitOfWork, IPropertyRepository propertyRepository, IHttpContextAccessor httpContextAccessor)
        {
            _propertyImageRepository = propertyImageRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _propertyRepository = propertyRepository;
        }

        public async Task<Unit> Handle(DeletePropertyPhotoCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);

            var propertyPhoto = await _propertyImageRepository.GetByIdAsync(command.Id, cancellationToken);
            if (propertyPhoto == null)
                throw new Exception("Image doesn't exist");

            var property = await _propertyRepository.GetByIdAsync(propertyPhoto.PropertyId, cancellationToken);
            if (property == null || property.OwnerId != userId)
                throw new Exception("You are not authorized to delete this image.");

            _propertyImageRepository.Delete(propertyPhoto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
