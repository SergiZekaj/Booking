using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Amenities;
using MediatR;

namespace Booking.Application.Features.Amenities.Command.Create
{
    internal class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, Guid>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAmenityCommandHandler(IAmenityRepository amenityRepository, IUnitOfWork unitOfWork)
        {
            _amenityRepository = amenityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateAmenityCommand command, CancellationToken cancellationToken)
        {

            if (await _amenityRepository.ExistsAsync(command.AmenityDto.Name, cancellationToken))
                throw new Exception("Amenity with this name already exists.");

            var amenity = new AmenityEntity
            {
                Id = Guid.NewGuid(),
                Name = command.AmenityDto.Name,
                Description = command.AmenityDto.Description
            };

            await _amenityRepository.AddAsync(amenity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return amenity.Id;
        }
    }
}
