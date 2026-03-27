using Booking.Application.Abstractions.Contracts;
using Booking.Application.Contracts;
using Booking.Domain.Bookings;
using Booking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Features.Bookings.Commands.Create
{
    internal class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookingCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IPropertyRepository propertyRepository,
            IUserRepository userRepository,
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var dto = command.BookingDto;

            var property = await _propertyRepository.GetByIdAsync(dto.PropertyId, cancellationToken);
            if (property == null)
                throw new Exception("Property not found.");
            if (!property.IsApproved)
                throw new Exception("Property must be approved to be booked.");

            if (dto.GuestCount > property.MaxGuests)
                throw new Exception("GuestCount cannot exceed MaxGuests.");

            if (await _bookingRepository.HasOverlappingBookingAsync(dto.PropertyId, dto.StartDate, dto.EndDate, cancellationToken))
                throw new Exception("This property is already booked for the selected dates.");

            var guest = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (guest == null)
                throw new Exception("User not found.");

            var nights = (dto.EndDate - dto.StartDate).Days;

            var booking = new BookingEntity
            {
                Id = Guid.NewGuid(),
                PropertyId = dto.PropertyId,
                Property = property,
                GuestId = userId,
                Guest = guest,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                GuestCount = dto.GuestCount,
                CleaningFee = 0,
                AmenitiesUpCharge = 0,
                PriceForPeriod = 0,
                TotalPrice = nights * dto.PricePerNight,
                BookingStatus = BookingStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _bookingRepository.AddAsync(booking, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }
    }
}

