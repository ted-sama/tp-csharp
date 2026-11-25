using System.Diagnostics;
using System.Globalization;

// 1.2 (5) Lecture de notes.csv avec ParserStricte
static List<Note> ImportNotesCSV(string chemin)
{
    List<Note> notesValides = new List<Note>();
    int numero = 1;
    foreach (string ligne in File.ReadLines(chemin).Skip(1))
    {
        numero++;
        try
        {
            notesValides.Add(ParserStricte(ligne, numero));
        }
        catch (FormatException e)
        {
            Console.Error.WriteLine(e.Message);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.Error.WriteLine($"Ligne {numero} : {e.Message}");
        }
    }
    return notesValides;
}

// 0.2 (1) Lecture des inscrits avec le code postal
List<Inscrit> inscrits = File.ReadLines("etudiants.csv")
    .Skip(1)
    .Select(ligne => ligne.Split(','))
    .Select(c => new Inscrit(c[0], c[1], c[2], c[3]))
    .ToList();

// Parser de la séance 2, remplacé par ParserStricte
// static Note? Parser(string ligne)
// {
//     var c = ligne.Split(',');
//     if (c.Length != 5) return null;
//     if (!double.TryParse(c[3], CultureInfo.InvariantCulture, out var v)) return null;
//     if (v < 0 || v > 20) return null;
//     if (!int.TryParse(c[4], out var coef)) return null;
//     return new Note(c[0], c[1], c[2], v, coef);
// }

// 1.1 (2) Test d'une note hors limites : ArgumentOutOfRangeException
// var note = new Note("Lovelace", "Ada", "Algo", 25, 3);

// 1.2 (4) Parser qui lève une FormatException avec le numéro de ligne
static Note ParserStricte(string ligne, int numero)
{
    var c = ligne.Split(',');
    if (c.Length != 5) throw new FormatException($"Ligne {numero} : nombre de colonnes incorrect");
    if (!double.TryParse(c[3], CultureInfo.InvariantCulture, out var v)) throw new FormatException($"Ligne {numero} : note invalide");
    if (!int.TryParse(c[4], out var coef)) throw new FormatException($"Ligne {numero} : coefficient invalide");
    return new Note(c[0], c[1], c[2], v, coef);
}

// 2 (8) Sauvegarde des notes dans un fichier JSON
// Stockage.Sauver("promo.json", notes);

// 2 (9) Lecture des notes depuis le fichier JSON si celui-ci existe, sinon on le crée à partir du CSV
// var notesInitiales = Stockage.Charger("promo.json");
// if (notesInitiales.Count == 0)
// {
//     Stockage.Sauver("promo.json", ImportNotesCSV("notes.csv"));
// }

// 3.1 (12) Pareil en asynchrone avec ChargerAsync et SauverAsync
List<Note> notes = await Stockage.ChargerAsync("promo.json");
if (notes.Count == 0)
{
    notes = ImportNotesCSV("notes.csv");
    await Stockage.SauverAsync("promo.json", notes);
}
Console.WriteLine($"{notes.Count} notes valides et {inscrits.Count} inscrits");

// 3.1 (13) Sans le await : warning CS4014, la sauvegarde n'est pas attendue
// Stockage.SauverAsync("promo.json", notes);

// 3.2 (14) Trois appels attendus l'un après l'autre
var chrono = Stopwatch.StartNew();
Console.WriteLine(await OperationLenteAsync("Opération 1"));
Console.WriteLine(await OperationLenteAsync("Opération 2"));
Console.WriteLine(await OperationLenteAsync("Opération 3"));
Console.WriteLine($"À la suite : {chrono.ElapsedMilliseconds} ms");

// 3.2 (15) Les trois appels lancés d'abord puis attendus ensemble avec Task.WhenAll
chrono.Restart();
Task<string> tache1 = OperationLenteAsync("Opération 1");
Task<string> tache2 = OperationLenteAsync("Opération 2");
Task<string> tache3 = OperationLenteAsync("Opération 3");
string[] resultats = await Task.WhenAll(tache1, tache2, tache3);
foreach (string resultat in resultats)
{
    Console.WriteLine(resultat);
}
Console.WriteLine($"Avec Task.WhenAll : {chrono.ElapsedMilliseconds} ms");

// 3.2 (16) Avec .Result au lieu de await (marche mais bloque le thread, interdit dans le rendu)
// chrono.Restart();
// Console.WriteLine(OperationLenteAsync("Opération 1").Result);
// Console.WriteLine(OperationLenteAsync("Opération 2").Result);
// Console.WriteLine(OperationLenteAsync("Opération 3").Result);
// Console.WriteLine($"Avec .Result : {chrono.ElapsedMilliseconds} ms");

// 3.2 Opération lente simulée (1 seconde)
static async Task<string> OperationLenteAsync(string nom)
{
    await Task.Delay(1000);
    return $"{nom} terminée";
}

// 4.1 (18) Communes du code postal 35000
var communes35000 = await GeoApi.CommunesAsync("35000");
foreach (var commune in communes35000)
{
    Console.WriteLine($"{commune.Nom} ({commune.Code}) : {commune.Population} habitants");
}

// 4.1 (19) Code postal inexistant : liste vide
var communes99999 = await GeoApi.CommunesAsync("99999");
foreach (var commune in communes99999)
{
    Console.WriteLine($"{commune.Nom} ({commune.Code}) : {commune.Population} habitants");
}

// 4.1 Communes du code postal 29200
var communes29200 = await GeoApi.CommunesAsync("29200");
foreach (var commune in communes29200)
{
    Console.WriteLine($"{commune.Nom} ({commune.Code}) : {commune.Population} habitants");
}

// 4.2 (20, 21) La commune de chaque étudiant, toutes les requêtes en même temps
// 4.3 (24) avec un délai maximal de 3 secondes pour l'ensemble des requêtes
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
var chrono2 = Stopwatch.StartNew();
try
{
    var resultatsCommunes = await Task.WhenAll(inscrits.Select(async inscrit =>
    {
        var communes = await GeoApi.CommunesAsync(inscrit.CodePostal, cts.Token);
        return (inscrit, communes);
    }));
    foreach (var (inscrit, communes) in resultatsCommunes)
    {
        Console.WriteLine($"{inscrit.Prenom} {inscrit.Nom} ({inscrit.Promo}) : {communes.FirstOrDefault()?.Nom ?? "commune inconnue"}");
    }
}
catch (OperationCanceledException)
{
    Console.Error.WriteLine("Délai dépassé, les requêtes ont été annulées");
}
chrono2.Stop();
Console.WriteLine($"En parallèle : {chrono2.ElapsedMilliseconds} ms");

// 4.2 (22) Version où les requêtes sont attendues l'une après l'autre
chrono2.Restart();
foreach (var inscrit in inscrits)
{
    var communes = await GeoApi.CommunesAsync(inscrit.CodePostal);
    Console.WriteLine($"{inscrit.Prenom} {inscrit.Nom} ({inscrit.Promo}) : {communes.FirstOrDefault()?.Nom ?? "commune inconnue"}");
}
chrono2.Stop();
Console.WriteLine($"L'une après l'autre : {chrono2.ElapsedMilliseconds} ms"); 