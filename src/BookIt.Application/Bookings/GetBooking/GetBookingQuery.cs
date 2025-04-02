﻿using BookIt.Application.Messaging;

namespace BookIt.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>;