using System.Text.Json;

// 2 (7) Stockage en JSON
public static class Stockage
{
    private static readonly JsonSerializerOptions Options = new (JsonSerializerOptions.Web) { WriteIndented = true };

    // 2 (8) Sauver et Charger
    public static void Sauver(string chemin, List<Note> notes)
    {
        var json = JsonSerializer.Serialize(notes, Options);
        File.WriteAllText(chemin, json);
    }

    public static List<Note> Charger(string chemin)
    {
        try
        {
            var json = File.ReadAllText(chemin);
            return JsonSerializer.Deserialize<List<Note>>(json, Options) ?? new List<Note>();
        }
        // 2 (10) JSON abîmé
        catch (JsonException)
        {
            Console.WriteLine($"Le fichier {chemin} n'est pas un JSON valide");
            return new List<Note>();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Le fichier {chemin} est introuvable");
            return new List<Note>();
        }
    }

    // 3.1 (12) Versions asynchrones
    public static async Task SauverAsync(string chemin, List<Note> notes)
    {
        await using FileStream flux = File.Create(chemin);
        await JsonSerializer.SerializeAsync(flux, notes, Options);
    }

    public static async Task<List<Note>> ChargerAsync(string chemin)
    {
        try
        {
            await using FileStream flux = File.OpenRead(chemin);
            return await JsonSerializer.DeserializeAsync<List<Note>>(flux, Options) ?? new List<Note>();
        }
        catch (JsonException)
        {
            Console.WriteLine($"Le fichier {chemin} n'est pas un JSON valide");
            return new List<Note>();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Le fichier {chemin} est introuvable");
            return new List<Note>();
        }
    }
}