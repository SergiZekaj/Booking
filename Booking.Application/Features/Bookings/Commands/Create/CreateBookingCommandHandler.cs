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
        private readonly IEmailService _emailService;

        public CreateBookingCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            IPropertyRepository propertyRepository,
            IUserRepository userRepository,
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _httpContextAccessor = httpContextAccessor;
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst("uid")!.Value);
            var dto = command.BookingDto;

            var property = await _propertyRepository.GetByIdWithDetailsAsync(dto.PropertyId, cancellationToken);
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

            await _emailService.SendEmailAsync(
                guest.Email,
                "Booking Confirmed",
                $"Hi {guest.FirstName}, your booking from {dto.StartDate:d} to {dto.EndDate:d} has been created successfully!"
                );

            await _emailService.SendEmailAsync(
                property.Owner.Email,
                "New Booking Request",
                $"Hi, you have a new booking request for {property.Name} from {dto.StartDate:d} to {dto.EndDate:d}."
                );

            return booking.Id;
        }
    }
}

