using BookIt.Application.Abstractions.Email;

namespace BookIt.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(BookIt.Domain.Users.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}