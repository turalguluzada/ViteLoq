namespace ViteLoq.Application.DTOs.UserManagement;

public class OperationResult
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();

    public static OperationResult Ok() => new OperationResult { Success = true };
    public static OperationResult Fail(params string[] errors) => new OperationResult { Success = false, Errors = errors };

}