﻿using Asp.Versioning;
using BookIt.Application.Bookings.GetBooking;
using BookIt.Application.Bookings.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookIt.Api.Controllers.Bookings;

[Authorize]
[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/bookings")]
public class BookingController : ControllerBase
{
    private readonly ISender _sender;

    public BookingController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);
        
        var result = await _sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ReserveBooking(
        ReserveBookingRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(  // mapping the request to command
            request.ApartmentId,
            request.UserId,
            request.StartDate,
            request.EndDate);
        
        var result = await _sender.Send(command, cancellationToken); // sending the command using mediator which triggers our command handler

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
    }
}