namespace BookIt.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(BookIt.Domain.Users.Email recipient, string subject, string body);
}