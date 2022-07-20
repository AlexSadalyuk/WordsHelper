namespace API.Models;

public class WordRequest
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int TranslateId { get; set; }
    public TranslateRequest Translate { get; set; }
}