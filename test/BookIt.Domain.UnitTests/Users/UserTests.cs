using BookIt.Domain.UnitTests.Infrastructure;
using BookIt.Domain.Users;
using BookIt.Domain.Users.Events;
using FluentAssertions;

namespace BookIt.Domain.UnitTests.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        // Act
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);
        
        // Assert
        user.FirstName.Should().Be(UserData.FirstName);
        user.LastName.Should().Be(UserData.LastName);
        user.Email.Should().Be(UserData.Email);
    }

    [Fact]
    public void Create_Should_RaiseUserCreatedDomainEvent()
    {
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

        var domainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);
        
        domainEvent.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void Create_Should_AddRegisteredRoleToUser()
    {
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

        user.Roles.Should().Contain(Role.Registered);
    }
}

