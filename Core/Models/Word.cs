using Core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Word : BaseEntity
{
    [Required]
    public string Text { get; set; } = null!;
    public Language Language { get; set; } = Language.EN;
    public int? TranslateId { get; set; }
    public Translate Translate { get; set; }
}