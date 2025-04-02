﻿using BookIt.Application.Abstractions.Data;
using BookIt.Application.Messaging;
using BookIt.Domain.Abstractions;
using Dapper;

namespace BookIt.Application.Bookings.GetBooking;

internal sealed class GetBookingQueryHandler : IqueryHandler<GetBookingQuery, BookingResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT 
                           id AS Id,
                           apartment_id AS ApartmentId,
                           user_id AS UserId,
                           status AS Status,
                           price_for_period_amount AS PriceAmount,
                           cleaning_fee_amount AS CleaningFeeAmount,
                           cleaning_fee_currency AS CleaningFeeCurrency,
                           amenities_up_charge_amount AS AmenitiesUpChargeAmount,
                           amenities_up_charge_currency AS AmenitiesUpChargeCurrency,
                           total_price_amount AS TotalPrice,
                           total_price_currency AS TotalPriceCurrency,
                           duration_start AS StartDate,
                           duration_end AS EndDate,
                           created_on_utc AS DateCreated
                           FROM bookings
                           WHERE id = @BookingId
                           """;

        var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(
            sql,
            new
            {
                request.BookingId
            });
        
        return booking;
    }
}