using BookIt.Application.Abstractions.Caching;
using BookIt.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookIt.Application.Abstractions.Behaviors;

internal sealed class QueryCachingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : ICachedQuery 
    where TResponse : Result
{
    private readonly ICacheService _cachedService; // used to implement cached site patterns
    
    private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger; 

    public QueryCachingBehavior(
        ICacheService cachedService, 
        ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    {
        _cachedService = cachedService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await _cachedService.GetAsync<TResponse>(
            request.CacheKey, 
            cancellationToken);
        
        string name = typeof(TRequest).Name;

        if (cachedResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", name);
            
            return cachedResult;
        }
        
        _logger.LogInformation("Cache miss for {Query}", name);
        
        var result = await next();

        if (result.IsSuccess)
        {
            await _cachedService.SetAsync(
                request.CacheKey, 
                result, 
                request.Expiration, 
                cancellationToken);
        }
        
        return result;
    }
}