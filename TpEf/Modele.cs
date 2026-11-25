public class Etudiant
{
    public int Id { get; set; } // clé primaire
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public required string Promo { get; set; }
    public string? Email { get; set; }
    public List<Note> Notes { get; set; } = new List<Note>();
}

public class Note
{
    public int Id { get; set; } // clé primaire
    public required string Matiere { get; set; }
    public double Valeur { get; set; }
    public int Coef { get; set; }
    public int EtudiantId { get; set; } // clé étrangère vers Etudiant
    public Etudiant Etudiant { get; set; } = null!; // navigation vers l'étudiant
}