using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Domain.UserEntry.Exceptions;

public class UserAlreadyHasActiveEntryException : ConflictException
{
    public Guid UserId { get; }
    public UserAlreadyHasActiveEntryException(Guid userId, string message = "User already has an active entry.")
        : base(message)
    {
        UserId = userId;
    }
}