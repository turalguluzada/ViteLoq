using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Application.Exceptions
{
    /// <summary>
    /// Generic exception to indicate that a use-case (service) execution failed unexpectedly.
    /// Suitable for logging and returning 400/500 depending on policy.
    /// </summary>
    public class UseCaseExecutionException : AppException
    {
        public UseCaseExecutionException(string message) : base(message) { }
        public UseCaseExecutionException(string message, Exception inner) : base(message, inner) { }
    }
}