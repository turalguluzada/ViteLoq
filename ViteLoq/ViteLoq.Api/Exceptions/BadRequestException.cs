using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Api.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(message) { }
    }
}