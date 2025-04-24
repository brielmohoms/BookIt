using BookIt.Application.Bookings.GetBooking;
using BookIt.Application.IntegrationTests.Infrastructure;
using BookIt.Domain.Bookings;
using FluentAssertions;

namespace BookIt.Application.IntegrationTests.Bookings;

public class GetBookingTests : BaseIntegrationTest
{
    private static readonly Guid BookingId = Guid.NewGuid();

    public GetBookingTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetBookings_ShouldReturnFailure_WhenBookingNotFound()
    {
        var command = new GetBookingQuery(BookingId);
        
        var result = await Sender.Send(command);
        
        result.Error.Should().Be(BookingErrors.NotFound);
    }
}