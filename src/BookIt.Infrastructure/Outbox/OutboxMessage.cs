﻿namespace BookIt.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    public OutboxMessage(Guid id, DateTime occuredOnUtc, string type, string content)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
        Content = content;
        Type = type;
    }

    public Guid Id { get; init; }
    
    public DateTime OccuredOnUtc { get; init; }
    
    public string Type {get; init; }
    
    public string Content { get; init; }
    
    public DateTime? ProcessedOnUtc { get; init; }
    
    public string? Error { get; init; }
    
}