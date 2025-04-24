using BookIt.Application.Bookings.ConfirmBooking;
using BookIt.Application.IntegrationTests.Infrastructure;
using BookIt.Domain.Bookings;
using FluentAssertions;

namespace BookIt.Application.IntegrationTests.Bookings;

public class ConfirmBookingTests : BaseIntegrationTest
{
    private static readonly Guid BookingId = Guid.NewGuid();

    public ConfirmBookingTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ConfirmBooking_ShouldReturnFailure_WhenBookingIsNotFound()
    {
        var command = new ConfirmBookingCommand(BookingId);
        
        var result = await Sender.Send(command);
        
        result.Error.Should().Be(BookingErrors.NotFound);
    }
}