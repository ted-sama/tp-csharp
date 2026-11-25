// 5.1 La classe Promo en LINQ
public class Promo(string nom, IEnumerable<Note> notes)
{
    private readonly List<Note> _notes = notes.ToList();
    public string Nom { get; } = nom;

    // prénoms distincts
    public IEnumerable<string> Etudiants() => _notes.Select(n => n.Prenom).Distinct();

    // moyenne pondérée, 0 si aucune note
    public double Moyenne(string prenom)
    {
        List<Note> notesEtudiant = _notes.Where(n => n.Prenom == prenom).ToList();
        if (notesEtudiant.Count == 0)
        {
            return 0;
        }
        return notesEtudiant.Sum(n => n.Valeur * n.Coef) / notesEtudiant.Sum(n => n.Coef);
    }

    // moyenne des moyennes des étudiants
    public double MoyenneGenerale() => Etudiants().Any() ? Etudiants().Average(p => Moyenne(p)) : 0;

    // prénom du major, null si promo vide
    public string? Major() => Etudiants().MaxBy(p => Moyenne(p));

    // (prénom, moyenne) par moyenne décroissante
    public IEnumerable<(string Prenom, double Moyenne)> Classement() =>
        Etudiants()
            .Select(p => (Prenom: p, Moyenne: Moyenne(p)))
            .OrderByDescending(c => c.Moyenne);

    // matière -> nombre de notes >= 10
    public IReadOnlyDictionary<string, int> NbAdmisParMatiere() =>
        _notes
            .GroupBy(n => n.Matiere)
            .ToDictionary(g => g.Key, g => g.Count(n => n.Valeur >= 10));

    // 5.2 "1. Grace 16,67 Très bien"
    public IEnumerable<string> Releve() =>
        Classement().Select((c, i) => $"{i + 1}. {c.Prenom} {c.Moyenne:F2} {Mention(c.Moyenne)}");

    // Mention de la séance 1
    private static string Mention(double moyenne)
    {
        return moyenne switch
        {
            < 0 or > 20 => throw new ArgumentOutOfRangeException(nameof(moyenne), "La moyenne doit être comprise entre 0 et 20."),
            >= 16 => "Très bien",
            >= 14 => "Bien",
            >= 12 => "Assez bien",
            >= 10 => "Passable",
            _ => "Ajourné"
        };
    }
}
