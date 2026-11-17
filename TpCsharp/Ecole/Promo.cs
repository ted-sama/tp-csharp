namespace Ecole;

public class Promo
{
    private readonly List<Etudiant> _etudiants = new List<Etudiant>();

    public string Nom { get; }

    public Promo(string nom)
    {
        Nom = nom;
    }

    public void Inscrire(Etudiant e)
    {
        _etudiants.Add(e);
    }

    public Etudiant? Chercher(string nom)
    {
        foreach (var etudiant in _etudiants)
        {
            if (string.Equals(etudiant.Nom, nom, StringComparison.OrdinalIgnoreCase))
            {
                return etudiant;
            }
        }
        return null;
    }

    public Etudiant? Major()
    {
        Etudiant? major = null;
        foreach (var etudiant in _etudiants)
        {
            if (major is null || etudiant.Moyenne() > major.Moyenne())
            {
                major = etudiant;
            }
        }
        return major;
    }

    public double MoyenneGenerale()
    {
        if (_etudiants.Count == 0)
        {
            return 0;
        }

        double somme = 0;
        foreach (var etudiant in _etudiants)
        {
            somme += etudiant.Moyenne();
        }
        return somme / _etudiants.Count;
    }

    public void AfficherReleve()
    {
        Console.WriteLine($"Relevé de la promo {Nom}");
        foreach (var etudiant in _etudiants)
        {
            Console.WriteLine($"{etudiant} - {ControleCollections.Mention(etudiant.Moyenne())}");
        }
        Console.WriteLine($"Moyenne générale : {MoyenneGenerale():F2}");
    }
}
