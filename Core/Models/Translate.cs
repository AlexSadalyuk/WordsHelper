using Core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Translate : BaseEntity
{
    [Required]
    public string Text { get; set; } = null!;
    public Language Language { get; set; } = Language.UA;
    [Required]
    public int WordId { get; set; }
    public Word Word { get; set; }
}