using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Infrastructure.Exceptions
{
    /// <summary>
    /// Wraps low-level database errors to avoid leaking provider types (EF exceptions) into higher layers.
    /// </summary>
    public class DatabaseException : AppException
    {
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, Exception inner) : base(message, inner) { }
    }
}