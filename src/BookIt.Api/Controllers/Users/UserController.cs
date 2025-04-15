using BookIt.Application.Users.GetLoggedInUser;
using BookIt.Application.Users.LoginUser;
using BookIt.Application.Users.RegisterUser;
using BookIt.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookIt.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender; 
    
    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("me")]
    [Authorize(Roles = Roles.Registered)]
    public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
    {
        var query = new GetLoggedInUserQuery();

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

     [AllowAnonymous] // so that anyone can register
     [HttpPost("register")]
     public async Task<IActionResult> Register(
         RegisterUserRequest request, 
         CancellationToken cancellationToken)
     {
         var command = new RegisterUserCommand(  // mapping the request into the command
             request.Email, 
             request.FirstName, 
             request.LastName, 
             request.Password); 
         
         var result = await _sender.Send(command, cancellationToken); // sending the command using mediator which triggers our command handler
         
         if (result.IsFailure) 
         { 
             return BadRequest(result.Error); 
         } 
         return Ok(result.Value);
     }

     [AllowAnonymous]
     [HttpPost("login")]
     public async Task<IActionResult> Login(
         LoginUserRequest request,
         CancellationToken cancellationToken)
     {
         var command = new LoginUserCommand(request.Email, request.Password);
         
         var result = await _sender.Send(command, cancellationToken);

         if (result.IsFailure)
         {
             return Unauthorized(result.Error);
         }
         
         return Ok(result.Value);
     }
}