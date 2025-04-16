﻿using BookIt.Application.Abstractions.Data;
using BookIt.Application.Abstractions.Messaging;
using BookIt.Domain.Abstractions;
using BookIt.Domain.Bookings;
using Dapper;

namespace BookIt.Application.Apartments.SearchApartments;

internal sealed class SearchApartmentsQueryHandler 
    : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private static readonly int[] ActiveBookingStatuses =
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed,
    };

    public SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken token)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<ApartmentResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT
                           a.id AS Id,
                           a.name AS Name,
                           a.description AS Description,
                           a.price_amount AS Price,
                           a.price_currency AS Currency,
                           a.address_country AS Country,
                           a.address_state AS State,
                           a.address_zip_code AS ZipCode,
                           a.address_city AS City,
                           a.address_street_number AS Street
                           FROM apartments AS a
                           WHERE NOT EXISTS(
                               SELECT 1
                               FROM bookings AS b
                               WHERE
                                   b.apartment_id = a.id AND 
                                   b.duration_start_date <= @EndDate AND
                                   b.duration_end_date >= @StartDate AND
                                   b.status = ANY(@ActiveBookingStatuses)
                           )
                           """;

        var apartments = await connection.QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
            sql,
            (apartment, address) =>
            {
                apartment.Address = address;
                return apartment;
            },
            new
            {
                request.StartDate,
                request.EndDate,
                ActiveBookingStatuses
            },
            splitOn: "Country"
       );
        return apartments.ToList();
    }
}