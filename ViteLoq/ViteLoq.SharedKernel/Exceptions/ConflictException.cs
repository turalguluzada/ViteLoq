using System;

namespace ViteLoq.SharedKernel.Exceptions
{
    /// <summary>
    /// Use when an operation conflicts with current state (e.g. unique constraint).
    /// Maps to HTTP 409.
    /// </summary>
    public class ConflictException : AppException
    {
        public ConflictException(string message) : base(message) { }

        public ConflictException(string message, Exception? inner) : base(message, inner) { }
    }
}