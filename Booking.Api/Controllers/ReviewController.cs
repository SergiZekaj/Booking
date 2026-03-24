using Booking.Application.Features.Reviews.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateReviewDto dto)
        {
            var result = await _mediator.Send(new CreateReviewCommand { ReviewDto = dto });
            return Ok(result);
        }
    }
}
