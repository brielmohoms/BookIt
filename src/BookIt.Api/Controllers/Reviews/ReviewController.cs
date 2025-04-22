using Asp.Versioning;
using BookIt.Application.Reviews.AddReview;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookIt.Api.Controllers.Reviews;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/reviews")]
public class ReviewController : ControllerBase
{
    private readonly ISender _sender;

    public ReviewController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddReview(AddReviewRequest request, CancellationToken cancellationToken)
    {
        var command = new AddReviewCommand(request.BookingId, request.Rating, request.Comment);
        
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok();
    }
}