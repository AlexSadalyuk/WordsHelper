namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

[ApiController]
[Route("[controller]")]
public class ExercisesController : ControllerBase
{

    private readonly IExerciseService _exerciseService;
    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
    }
    public async Task<IActionResult> Index()
    {
        var exercises = await _exerciseService.GetExercises();

        return Ok(exercises);
    }
}
