using System.Net.Http.Json;

public static class GeoApi
{
    // 4.1 (18) Un seul HttpClient pour tout le programme
    private static readonly HttpClient Client = new HttpClient
    {
        BaseAddress = new Uri("https://geo.api.gouv.fr/")
    };

    // 4.1 (19) Communes d'un code postal, liste vide si aucune
    public static async Task<List<Commune>> CommunesAsync(string codePostal, CancellationToken jeton = default)
    {
        try
        {
            var communes = await Client.GetFromJsonAsync<List<Commune>>($"communes?codePostal={codePostal}", jeton);
            return communes ?? new List<Commune>();
        }
        // 4.3 (23) Réseau ou adresse injoignable
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erreur lors de l'appel à l'API : {e.Message}");
            return new List<Commune>();
        }
    }
}