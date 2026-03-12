using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Application.Features.Users.Queries.GetMyProfile;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Users.Commands.Update
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetMyProfileDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetMyProfileDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            
            if (user == null)
                throw new Exception("User not found.");

            if(command.Update.FirstName != null)
                user.FirstName = command.Update.FirstName;

            if(command.Update.LastName != null)
                user.LastName = command.Update.LastName;

            if(command.Update.PhoneNumber != null)
                user.PhoneNumber = command.Update.PhoneNumber;

            if(command.Update.ProfileImageUrl != null)
                user.ProfileImageUrl = command.Update.ProfileImageUrl;

            user.LastModifiedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetMyProfileDto>(user);
        }
    }
}
