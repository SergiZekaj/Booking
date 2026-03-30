using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Admin.Queries.GetAllOwnerProfiles
{
    internal class GetAllOwnerProfilesQueryHandler : IRequestHandler<GetAllOwnerProfilesQuery, List<GetAllOwnerProfilesDto>>
    {
        private readonly IOwnerProfileRepository _ownerProfileRepository;

        public GetAllOwnerProfilesQueryHandler(IOwnerProfileRepository ownerProfileRepository)
        {
            _ownerProfileRepository = ownerProfileRepository;
        }

        public async Task<List<GetAllOwnerProfilesDto>> Handle(GetAllOwnerProfilesQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _ownerProfileRepository.GetAllAsync(cancellationToken);

            return profiles.Select(op => new GetAllOwnerProfilesDto
            {
                UserId = op.UserId,
                FullName = $"{op.User.FirstName} {op.User.LastName}",
                Email = op.User.Email,
                IdentityCardNumber = op.IdentityCardNumber,
                BusinessName = op.BusinessName,
                VerificationStatus = op.VerificationStatus,
                CreatedAt = op.CreatedAt
            }).ToList();
        }
    }
}