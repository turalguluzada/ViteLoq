using System;

namespace ViteLoq.SharedKernel.Exceptions
{
    /// <summary>
    /// Use when the current principal is not authorized to perform an action.
    /// Maps to HTTP 401 / 403 (your middleware decides).
    /// </summary>
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception? inner) : base(message, inner) { }
    }
}