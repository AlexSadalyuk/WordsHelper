using API.Models;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("[controller]")]
public class WordsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISpecificationBuilder<Word> _specificationBuilder;

    public WordsController(IUnitOfWork unitOfWork, ISpecificationBuilder<Word> specificationBuilder)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _specificationBuilder = specificationBuilder ?? throw new ArgumentNullException(nameof(specificationBuilder));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var spec = _specificationBuilder
            .AddInclude(item => item.Translate)
            .GetSpecification();

        var result = await _unitOfWork.Repository<Word>().Get(spec);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _unitOfWork.Repository<Word>().Get(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] WordRequest model)
    {
        var newItem = await _unitOfWork.Repository<Word>().Add(new Word
        {
            Text = model.Text,
            Language = Language.EN,
            Translate = new Translate
            {
                Text = model.Translate.Text,
                Language = Language.UA
            }
        });

        await _unitOfWork.SaveChanges();

        return Ok(newItem);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] WordRequest model)
    {
        var spec = _specificationBuilder
        .AddInclude(item => item.Translate)
        .AddCriteria(item => item.Id == model.Id)
        .GetSpecification();

        var item = (await _unitOfWork.Repository<Word>().Get(spec))
            .FirstOrDefault();

        if (item is null) return BadRequest("Can't find item for update");

        item.Text = model.Text;
        item.Translate.Text = model.Translate.Text;

        _unitOfWork.Repository<Word>().Update(item);
        await _unitOfWork.SaveChanges();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _unitOfWork.Repository<Word>().Delete(id);
        await _unitOfWork.SaveChanges();
        return Ok(item);
    }
}