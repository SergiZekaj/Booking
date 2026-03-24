using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Application.Features.OwnerProfile.Commands.Update;
using Booking.Application.Features.OwnerProfile.Queries.GetMyOwnerProfile;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.OwnerProfile.Commands.Update 
{
    public class UpdateOwnerProfileCommandHandler : IRequestHandler<UpdateOwnerProfileCommand, GetMyOwnerProfileDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOwnerProfileCommandHandler(IOwnerProfileRepository ownerProfileRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetMyOwnerProfileDto> Handle(UpdateOwnerProfileCommand command, CancellationToken cancellationToken)
        {
            var profileId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var profile = await _ownerProfileRepository.GetByUserIdAsync(profileId, cancellationToken);

            if (profile == null)
                throw new Exception("Profile not found.");

            if(command.UpdateDto.BusinessName != null)
                profile.BusinessName = command.UpdateDto.BusinessName;

            if(command.UpdateDto.IdentityCardNumber != null)
                profile.IdentityCardNumber = command.UpdateDto.IdentityCardNumber;

            profile.LastModifiedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GetMyOwnerProfileDto>(profile);
        }
    }
}