
using PassNester.Models;
using System.IO;
using System.Text.Json;

namespace PassNester.Services;

public class PasswordStorageService
{
    private static readonly string FilePath = "passwords.json";

    public static List<PasswordCollection> Load()
    {
        if (!File.Exists(FilePath))
        {
            return new List<PasswordCollection>();
        }
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<PasswordCollection>>(json) ?? new List<PasswordCollection>();
    }

    public static void Save(List<PasswordCollection> collections)
    {
        var json = JsonSerializer.Serialize(collections, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}