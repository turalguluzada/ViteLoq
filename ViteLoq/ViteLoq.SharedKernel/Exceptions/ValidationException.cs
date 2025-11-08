using System;
using System.Collections.Generic;

namespace ViteLoq.SharedKernel.Exceptions
{
    /// <summary>
    /// Represents validation failures. Contains field->errors dictionary.
    /// Maps to HTTP 400 and can be serialized to ProblemDetails.errors.
    /// </summary>
    public class ValidationException : AppException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException()
            : base("One or more validation errors occurred.")
        {
            Errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : this()
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }
}