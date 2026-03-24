using AutoMapper;
using Booking.Application.Abstractions.Contracts;
using MediatR;

namespace Booking.Application.Features.Reviews.Queries.GetPropertyReviews
{
    internal class GetPropertyReviewsQueryHandler : IRequestHandler<GetPropertyReviewsQuery, List<GetPropertyReviewsDto>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GetPropertyReviewsQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<List<GetPropertyReviewsDto>> Handle(GetPropertyReviewsQuery query, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetByPropertyIdAsync(query.PropertyId, cancellationToken);
            return _mapper.Map<List<GetPropertyReviewsDto>>(reviews);
        }
    }
}
