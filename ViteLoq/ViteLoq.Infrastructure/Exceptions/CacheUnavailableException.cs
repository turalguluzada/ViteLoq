using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Infrastructure.Exceptions;

public class CacheUnavailableException : AppException
{
    public CacheUnavailableException(string message) : base(message) { }
    public CacheUnavailableException(string message, Exception inner) : base(message, inner) { }
}