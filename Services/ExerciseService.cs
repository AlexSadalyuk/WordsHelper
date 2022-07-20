using Core.Interfaces;
using Core.Models;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class ExerciseService : IExerciseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpecificationBuilder<Word> _builder;

    public ExerciseService(IUnitOfWork unitOfWork, ISpecificationBuilder<Word> builder)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
    }

    public async Task<IReadOnlyCollection<WordDto>> GetExercises(int amount = 10)
    {
        var spec = _builder
        .AddInclude(word => word.Translate)
        .GetSpecification();

        var allWords = (await _unitOfWork.Repository<Word>()
            .Get(spec)).ToArray();

        var from = 0;
        var to = allWords.Count();
        var random = new Random();

        var collection = new List<WordDto>();

        for (var i = 0; i < amount; i++)
        {
            var item = allWords[random.Next(from, to)];
            if(collection.Any(word => word.Id == item.Id))
            {
                i--;
                continue;
            }
            collection.Add(
                new WordDto
                {
                    Id = item.Id,
                    Translate = item.Translate.Text,
                    Word = item.Text
                }
            );
        }

        return collection.AsReadOnly();
    }
}