using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Infrastructure.Exceptions;

public class DbConcurrencyException : ConflictException
{
    public DbConcurrencyException(string message) : base(message) { }
    public DbConcurrencyException(string message, Exception inner) : base(message, inner) { }
}