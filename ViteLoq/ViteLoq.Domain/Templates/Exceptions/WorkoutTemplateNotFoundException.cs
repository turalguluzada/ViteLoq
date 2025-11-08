using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Domain.Templates.Exceptions;

public class WorkoutTemplateNotFoundException : NotFoundException
{
    public Guid TemplateId { get; }
    public WorkoutTemplateNotFoundException(Guid id)
        : base($"Workout template with id '{id}' was not found.")
    {
        TemplateId = id;
    }
}