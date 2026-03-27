using Booking.Application.Features.Bookings.Commands.Cancel;
using Booking.Application.Features.Bookings.Commands.Confirm;
using Booking.Application.Features.Bookings.Commands.Create;
using Booking.Application.Features.Bookings.Commands.Reject;
using Booking.Application.Features.Bookings.Queries.GetMyBookings;
using Booking.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateBookingDto dto)
        {
            var id = await _mediator.Send(new CreateBookingCommand { BookingDto = dto });
            return Ok(id);
        }

        [HttpGet("my-bookings")]
        public async Task<ActionResult<List<GetMyBookingsDto>>> GetMyBookings([FromQuery] BookingStatus? status)
        {
            var result = await _mediator.Send(new GetMyBookingsQuery { Status = status });
            return Ok(result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelBookingCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmBookingCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("reject")]
        public async Task<IActionResult> Reject([FromBody] RejectBookingCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

