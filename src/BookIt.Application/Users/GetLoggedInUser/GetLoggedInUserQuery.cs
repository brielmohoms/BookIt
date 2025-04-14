using BookIt.Application.Abstractions.Messaging;

namespace BookIt.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
