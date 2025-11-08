using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Application.Exceptions;

public class OperationFailedException : AppException
{
    public IEnumerable<string> Errors { get; }

    public OperationFailedException(string message) : base(message)
    {
        Errors = Array.Empty<string>();
    }

    public OperationFailedException(IEnumerable<string> errors) : base("Operation failed")
    {
        Errors = errors ?? Array.Empty<string>();
    }
}