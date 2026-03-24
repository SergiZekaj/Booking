using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Bookings;
using Booking.Domain.Enums;
using Booking.Domain.Reviews;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Reviews.Command.Create
{
    internal class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericRepository<BookingEntity> _bookingRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewCommandHandler(IHttpContextAccessor httpContextAccessor, IGenericRepository<BookingEntity> bookingRepository, IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookingRepository = bookingRepository;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);

            var booking = await _bookingRepository.GetByIdAsync(command.ReviewDto.BookingId, cancellationToken);
            if (booking == null)
                throw new Exception("Booking not found.");
            if (booking.GuestId != userId)
                throw new Exception("You are not authorized.");
            if (booking.BookingStatus != BookingStatus.Completed)
                throw new Exception("Booking must be completed before reviewing.");
            if (await _reviewRepository.ExistsByBookingIdAsync(command.ReviewDto.BookingId, cancellationToken))
                throw new Exception("You have already reviewed this booking.");

            var review = new ReviewEntity
            {
                Id = Guid.NewGuid(),
                BookingId = command.ReviewDto.BookingId,
                GuestId = userId,
                Rating = command.ReviewDto.Rating,
                Comment = command.ReviewDto.Comment,
            };

            await _reviewRepository.AddAsync(review, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return review.Id;
        }
    }
}
