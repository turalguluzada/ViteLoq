using System;

namespace ViteLoq.SharedKernel.Exceptions
{
    /// <summary>
    /// Base class for all custom application exceptions.
    /// Keep this in SharedKernel so all layers can depend on it.
    /// </summary>
    public abstract class AppException : Exception
    {
        protected AppException(string message) : base(message) { }

        protected AppException(string message, Exception? innerException) 
            : base(message, innerException) { }
    }
}