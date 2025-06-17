namespace PassNester.Models;

public class PasswordCollection
{
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;

    public List<PasswordEntry>? Entries { get; set; }
}