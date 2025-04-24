using BookIt.Application.Apartments.SearchApartments;
using BookIt.Application.IntegrationTests.Infrastructure;
using FluentAssertions;

namespace BookIt.Application.IntegrationTests.Apartments;

public class SearchApartmentsTests : BaseIntegrationTest
{
    public SearchApartmentsTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task SearchApartmentsShouldReturnEmptyList_WhenDateRangeIsInvalid()
    {
        var query = new SearchApartmentsQuery(new DateOnly(2025, 5, 10), new DateOnly(2025, 5, 1));

        var result = await Sender.Send(query);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }
    
    [Fact]
    public async Task SearchApartmentsShouldReturnEmptyList_WhenDateRangeIsValid()
    {
        var query = new SearchApartmentsQuery(new DateOnly(2025, 5, 1), new DateOnly(2025, 5, 10));

        var result = await Sender.Send(query);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}