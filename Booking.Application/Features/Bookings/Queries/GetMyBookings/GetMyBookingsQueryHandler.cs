using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Features.Bookings.Queries.GetMyBookings
{
    internal class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, List<GetMyBookingsDto>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetMyBookingsQueryHandler(
            IHttpContextAccessor httpContextAccessor,
            IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<List<GetMyBookingsDto>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
        {
            var uidClaim = _httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value;
            var userId = Guid.Parse(uidClaim);

            var bookings = await _bookingRepository.GetMyBookingsAsync(userId, request.Status, cancellationToken);
            return _mapper.Map<List<GetMyBookingsDto>>(bookings);
        }
    }
}

