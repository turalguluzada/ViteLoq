using System;

namespace ViteLoq.SharedKernel.Exceptions
{
    /// <summary>
    /// Use when an entity/resource cannot be found.
    /// Maps naturally to HTTP 404 in the API layer.
    /// </summary>
    public class NotFoundException : AppException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception? inner) : base(message, inner) { }
    }
}