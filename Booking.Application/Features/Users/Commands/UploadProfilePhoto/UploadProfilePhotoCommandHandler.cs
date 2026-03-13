using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Users.Commands.UploadProfilePhoto
{
    internal class UploadProfilePhotoCommandHandler : IRequestHandler<UploadProfilePhotoCommand, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadProfilePhotoCommandHandler(IHttpContextAccessor httpcontextAccessor, ICloudinaryService cloudinaryService,
        IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpcontextAccessor;
            _cloudinaryService = cloudinaryService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        } 

        public async Task<string> Handle(UploadProfilePhotoCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (userId == null)
                throw new Exception("User not found.");

            var imageUrl = await _cloudinaryService.UploadImageAsync(command.File);

            user.ProfileImageUrl = imageUrl;
            user.LastModifiedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return imageUrl;
        }
    }
}
