using BookIt.Application.Abstractions.Clock;

namespace BookIt.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
}