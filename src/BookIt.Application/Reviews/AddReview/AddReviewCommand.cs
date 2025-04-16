using BookIt.Application.Abstractions.Messaging;

namespace BookIt.Application.Reviews.AddReview;

public sealed record AddReviewCommand (
    Guid BookingId, 
    int Rating, 
    string Comment) : ICommand;