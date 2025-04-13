using BookIt.Domain.Abstractions;
using MediatR;

namespace BookIt.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}