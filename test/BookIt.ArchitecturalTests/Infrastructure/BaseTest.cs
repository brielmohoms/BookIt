using System.Reflection;
using BookIt.Application.Abstractions.Messaging;
using BookIt.Domain.Abstractions;
using BookIt.Infrastructure;

namespace BookIt.ArchitecturalTests.Infrastructure;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
    
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;
    
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}