using BookIt.Application.Users.LoginUser;
using BookIt.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
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

     [AllowAnonymous] // so that anyone can register
     [HttpPost("register")]
     public async Task<IActionResult> Register(
         RegisterUserRequest request)
     {
         var command = new RegisterUserCommand(  // mapping the request into the command
             request.Email, 
             request.FirstName, 
             request.LastName, 
             request.Password); 
         
         var result = await _sender.Send(command); // sending the command using mediator which triggers our command handler
         
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