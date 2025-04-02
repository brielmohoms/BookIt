using BookIt.Domain.Abstractions;
using MediatR;

namespace BookIt.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}