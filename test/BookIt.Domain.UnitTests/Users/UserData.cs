﻿using BookIt.Domain.Users;

namespace BookIt.Domain.UnitTests.Users;

internal static class UserData
{
    public static readonly FirstName FirstName = new("First");
    
    public static readonly LastName LastName = new("Last");
    
    public static readonly Email Email = new("test@mail.de");
}