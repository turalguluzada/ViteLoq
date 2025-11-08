using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Infrastructure.Exceptions;

public class StorageException : AppException
{
    public string? Key { get; }
    public StorageException(string key, string message) : base(message) => Key = key;
    public StorageException(string key, string message, Exception inner) : base(message, inner) => Key = key;
}