using BookIt.Domain.Abstractions;
using MediatR;

namespace BookIt.Application.Messaging;

public interface IqueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery : IQuery<TResponse>
{
}