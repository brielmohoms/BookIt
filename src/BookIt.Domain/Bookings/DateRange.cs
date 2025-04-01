﻿namespace BookIt.Domain;

public record DateRange
{
    private DateRange() { }
    
    public DateOnly StartDate { get; init; }
    
    public DateOnly EndDate { get; init; }
    
    public int LengthInDays => EndDate.DayNumber - StartDate.DayNumber;

    public static DateRange Create(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
        {
            throw new ApplicationException("Start date should be before end date");
        }

        // other exceptions 
        
        return new DateRange
        {
            StartDate = startDate,
            EndDate = endDate
        };
    }
}