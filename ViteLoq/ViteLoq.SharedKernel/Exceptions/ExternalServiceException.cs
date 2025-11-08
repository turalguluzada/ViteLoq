using System;

namespace ViteLoq.SharedKernel.Exceptions
{
    /// <summary>
    /// Generic wrapper for failures coming from external systems.
    /// Infrastructure can rethrow infra-specific exceptions as this common type.
    /// </summary>
    public class ExternalServiceException : AppException
    {
        public string ServiceName { get; }

        public ExternalServiceException(string serviceName, string message)
            : base($"{serviceName}: {message}")
        {
            ServiceName = serviceName;
        }

        public ExternalServiceException(string serviceName, string message, Exception inner)
            : base($"{serviceName}: {message}", inner)
        {
            ServiceName = serviceName;
        }
    }
}