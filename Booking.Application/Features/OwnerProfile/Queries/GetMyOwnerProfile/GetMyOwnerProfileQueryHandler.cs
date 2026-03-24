using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.OwnerProfile.Queries.GetMyOwnerProfile
{
    public class GetMyOwnerProfileQueryHandler : IRequestHandler<GetMyOwnerProfileQuery, GetMyOwnerProfileDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IMapper _mapper;

        public GetMyOwnerProfileQueryHandler(IOwnerProfileRepository ownerProfileRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ownerProfileRepository = ownerProfileRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetMyOwnerProfileDto> Handle(GetMyOwnerProfileQuery query, CancellationToken cancellationToken)
        {
            var profileId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var profile = await _ownerProfileRepository.GetByUserIdAsync(profileId, cancellationToken);

            if (profile == null)
                throw new Exception("Profile not found.");

            return _mapper.Map<GetMyOwnerProfileDto>(profile);
        }
    }
}
