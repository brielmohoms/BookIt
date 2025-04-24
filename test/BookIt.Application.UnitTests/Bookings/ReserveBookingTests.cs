using BookIt.Application.Abstractions.Clock;
using BookIt.Application.Bookings.ReserveBooking;
using BookIt.Application.Exceptions;
using BookIt.Application.UnitTests.Apartments;
using BookIt.Application.UnitTests.Users;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Apartments;
using BookIt.Domain.Bookings;
using BookIt.Domain.Users;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace BookIt.Application.UnitTests.Bookings;

public class ReserveBookingTests
{
    private static readonly DateTime Now = DateTime.Now;

    private static readonly ReserveBookingCommand Command = new(
        Guid.NewGuid(),
        Guid.NewGuid(),
        new DateOnly(2025, 5, 1),
        new DateOnly(2025, 5, 10));

    private readonly ReserveBookingCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IApartmentRepository _apartmentRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IBookingRepository _bookingRepositoryMock;
    private readonly PricingService _pricingService;
    private readonly IDateTimeProvider _dateTimeProviderMock;

    public ReserveBookingTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _apartmentRepositoryMock = Substitute.For<IApartmentRepository>();
        _bookingRepositoryMock = Substitute.For<IBookingRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
        _dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        _dateTimeProviderMock.Now.Returns(Now);

        _handler = new ReserveBookingCommandHandler(
            _userRepositoryMock,
            _apartmentRepositoryMock,
            _unitOfWorkMock,
            _bookingRepositoryMock,
            new PricingService(),
            _dateTimeProviderMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserIsNull()
    {
        // Arrange
        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns((User?)null);
        
        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        result.Error.Should().Be(UserErrors.NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenApartmentIsNull()
    {
        var user = UserData.Create();
        
        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        
        _apartmentRepositoryMock
            .GetByIdAsync(Command.ApartmentId, Arg.Any<CancellationToken>())
            .Returns((Apartment?)null);
        
        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        result.Error.Should().Be(ApartmentErrors.NotFound);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenApartmentIsBooked()
    {
        var user = UserData.Create();
        var apartment = ApartmentData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);
        
        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        
        _apartmentRepositoryMock
            .GetByIdAsync(Command.ApartmentId, Arg.Any<CancellationToken>())
            .Returns(apartment);
        
        _bookingRepositoryMock
            .IsOverlappingAsync(apartment, duration, Arg.Any<CancellationToken>())
            .Returns(true);
        
        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        result.Error.Should().Be(BookingErrors.Overlap);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkThrows()
    {
        var user = UserData.Create();
        var apartment = ApartmentData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);
        
        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        
        _apartmentRepositoryMock
            .GetByIdAsync(Command.ApartmentId, Arg.Any<CancellationToken>())
            .Returns(apartment);
        
        _bookingRepositoryMock
            .IsOverlappingAsync(apartment, duration, Arg.Any<CancellationToken>())
            .Returns(false);
        
        _unitOfWorkMock
            .SaveChangesAsync()
            .ThrowsAsync(new ConcurrencyException("Concurrency", new Exception()));
        
        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        result.Error.Should().Be(BookingErrors.Overlap);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenBookingIsReserved()
    {
        var user = UserData.Create();
        var apartment = ApartmentData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);
        
        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        
        _apartmentRepositoryMock
            .GetByIdAsync(Command.ApartmentId, Arg.Any<CancellationToken>())
            .Returns(apartment);
        
        _bookingRepositoryMock
            .IsOverlappingAsync(apartment, duration, Arg.Any<CancellationToken>())
            .Returns(false);
        
        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallRepository_WhenBookingIsReserved()
    {
        var user = UserData.Create();
        var apartment = ApartmentData.Create();
        var duration = DateRange.Create(Command.StartDate, Command.EndDate);
        
        _userRepositoryMock
            .GetByIdAsync(Command.UserId, Arg.Any<CancellationToken>())
            .Returns(user);
        
        _apartmentRepositoryMock
            .GetByIdAsync(Command.ApartmentId, Arg.Any<CancellationToken>())
            .Returns(apartment);
        
        _bookingRepositoryMock
            .IsOverlappingAsync(apartment, duration, Arg.Any<CancellationToken>())
            .Returns(false);
        
        // Act
        var result = await _handler.Handle(Command, default);
        
        // Assert
        _bookingRepositoryMock.Received(1).AddBooking(Arg.Is<Booking>(b => b.Id == result.Value));
    }
}
