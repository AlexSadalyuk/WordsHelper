using System.ComponentModel.DataAnnotations;

namespace WordsHelper.Core.Models;

public class BaseEntity 
{
    [Key]
    public int Id { get; set; }
}