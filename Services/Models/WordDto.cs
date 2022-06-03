namespace Services.Models;

public class WordDto
{
    public int Id { get; set; }
    public string Word { get; set; } = null!;
    public string Translate { get; set; } = null!;
}