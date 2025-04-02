using BookIt.Application.Abstractions.Behaviors;

namespace BookIt.Application.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> validationErrors)
    {
        Errors = validationErrors;
    }
    
    public IEnumerable<ValidationError> Errors { get; }
}