using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Booking.Application.Features.Users.Queries.GetMyProfile
{
    internal class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, GetMyProfileDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetMyProfileQueryHandler(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IMapper mapper) 
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetMyProfileDto> Handle(GetMyProfileQuery query, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);


            if (user == null)
                throw new Exception("User not found.");

            return _mapper.Map<GetMyProfileDto>(user);
        }
    }

}
