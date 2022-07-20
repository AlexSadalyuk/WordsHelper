using Services.Models;

namespace Services.Interfaces;

public interface IExerciseService
{
    Task<IReadOnlyCollection<WordDto>> GetExercises(int amount = 10);
}