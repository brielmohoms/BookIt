using BookIt.Domain.Abstractions;
using MediatR;

namespace BookIt.Application.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IBaseCommand
{
}