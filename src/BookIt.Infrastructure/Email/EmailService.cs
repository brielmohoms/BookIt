using BookIt.Application.Abstractions.Email;

namespace BookIt.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendEmailAsync(Domain.Users.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}