namespace TechLibrary.Api.Domain.Entities;

public class Book : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Amount { get; set; }
}
