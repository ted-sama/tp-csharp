namespace Ecole;

// 4.1 La classe Etudiant
public class Etudiant
{
    private readonly List<double> _notes = new List<double>();
    private int _annee;

    public string Nom { get; }
    public string Prenom { get; }
    public DateOnly Naissance { get; init; }

    public int Annee
    {
        get { return _annee; }
        set
        {
            if (value < 1 || value > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "L'année doit être comprise entre 1 et 5.");
            }
            _annee = value;
        }
    }

    public string NomComplet => $"{Prenom} {Nom.ToUpper()}";

    public int NbNotes => _notes.Count;

    public IReadOnlyList<double> Notes => _notes.AsReadOnly();

    public Etudiant(string nom, string prenom, int annee)
    {
        if (string.IsNullOrWhiteSpace(nom))
        {
            throw new ArgumentException("Le nom ne peut pas être vide.", nameof(nom));
        }
        if (string.IsNullOrWhiteSpace(prenom))
        {
            throw new ArgumentException("Le prénom ne peut pas être vide.", nameof(prenom));
        }

        Nom = nom;
        Prenom = prenom;
        Annee = annee;
    }

    public void AjouterNote(double n)
    {
        if (n < 0 || n > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "La note doit être comprise entre 0 et 20.");
        }
        _notes.Add(n);
    }

    public double Moyenne()
    {
        if (_notes.Count == 0)
        {
            return 0;
        }

        double somme = 0;
        foreach (var note in _notes)
        {
            somme += note;
        }
        return somme / _notes.Count;
    }

    public override string ToString()
    {
        string niveau = Annee <= 3 ? $"L{Annee}" : $"M{Annee - 3}";
        return $"{NomComplet} ({niveau}) : {Moyenne():F2}";
    }
}
