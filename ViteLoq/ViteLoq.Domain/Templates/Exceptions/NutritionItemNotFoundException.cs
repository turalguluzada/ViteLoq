using ViteLoq.SharedKernel.Exceptions;

namespace ViteLoq.Domain.Templates.Exceptions;

public class NutritionItemNotFoundException : NotFoundException
{
    public Guid NutritionItemId { get; }
    public NutritionItemNotFoundException(Guid id)
        : base($"Nutrition item with id '{id}' was not found.")
    {
        NutritionItemId = id;
    }
}