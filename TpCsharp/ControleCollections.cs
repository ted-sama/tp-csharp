class ControleCollections
{
    // 3.1 Mention
    public static string Mention(double moyenne)
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

    // 3.3 Moyenne utilisée pour le dictionnaire
    public static double Moyenne(List<double> notes)
    {
        if (notes.Count == 0)
        {
            return 0;
        }

        double somme = 0;
        foreach (var note in notes)
        {
            somme += note;
        }
        return somme / notes.Count;
    }

    // 3.2 Moyenne, min et max
    public static void CalculMoyenneEtMention(List<double> notes)
    {
        if (notes == null || notes.Count == 0)
        {
            throw new ArgumentException("La liste des notes ne peut pas être vide.", nameof(notes));
        }

        double somme = 0;
        double min = notes[0];
        double max = notes[0];
        foreach (var note in notes)
        {
            somme += note;
            if (note < min)
            {
                min = note;
            }
            if (note > max)
            {
                max = note;
            }
        }
        double moyenne = somme / notes.Count;

        string mention = Mention(moyenne);

        Console.WriteLine($"Moyenne : {moyenne:F2}, Min : {min}, Max : {max}, Mention : {mention}");
    }

    // 3.5 Filtrage par motif
    public static string Decrire(object? o)
    {
        return o switch
        {
            null => "null",
            int n when n % 2 == 0 => "entier pair",
            int => "entier impair",
            string { Length: 0 } => "chaîne vide",
            string s => $"chaîne de {s.Length} caractères",
            List<double> liste => $"liste de {liste.Count} éléments",
            _ => "autre"
        };
    }
}
