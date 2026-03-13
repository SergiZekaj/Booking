using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.PropertyImage;
using MediatR;

namespace Booking.Application.Features.Property.Commands.UploadPhoto
{
    internal class UploadPropertyPhotoCommandHandler : IRequestHandler<UploadPropertyPhotoCommand, string>
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadPropertyPhotoCommandHandler(ICloudinaryService cloudinaryService, IPropertyRepository propertyRepository, IUnitOfWork unitOfWork, IPropertyImageRepository propertyImageRepository)
        {
            _cloudinaryService = cloudinaryService;
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<string> Handle(UploadPropertyPhotoCommand command, CancellationToken cancellationToken)
        { 
            var property = await _propertyRepository.GetByIdAsync(command.PropertyId, cancellationToken);
            if (property == null)
                throw new Exception("Property not found.");

            var imageUrl = await _cloudinaryService.UploadImageAsync(command.File);

            var propertyImage = new PropertyImageEntity
            {
                Id = Guid.NewGuid(),
                PropertyId = command.PropertyId,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.UtcNow,
            };

            await _propertyImageRepository.AddAsync(propertyImage, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return imageUrl;
        }
    }
}
