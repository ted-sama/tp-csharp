using System.Globalization;

namespace Ecole;

public static class LecteurNotes
{
    // 6.3 Découpe et vérification d'une ligne
    public static Note LireLigne(string ligne, int numero)
    {
        string[] champs = ligne.Split(',');
        if (champs.Length != 5)
        {
            throw new NoteInvalideException($"5 champs attendus, {champs.Length} trouvés.", numero);
        }

        if (!double.TryParse(champs[3], NumberStyles.Float, CultureInfo.InvariantCulture, out double valeur))
        {
            throw new NoteInvalideException($"'{champs[3]}' n'est pas une note valide.", numero);
        }
        if (valeur < 0 || valeur > 20)
        {
            throw new NoteInvalideException($"La note {valeur} n'est pas entre 0 et 20.", numero);
        }

        if (!int.TryParse(champs[4], out int coef))
        {
            throw new NoteInvalideException($"'{champs[4]}' n'est pas un coefficient valide.", numero);
        }

        return new Note(champs[0], champs[1], champs[2], valeur, coef);
    }

    // 6.4 Lecture du fichier, les lignes invalides sont ignorées
    public static List<Note> Lire(string chemin)
    {
        List<Note> notes = new List<Note>();

        try
        {
            using StreamReader reader = new StreamReader(chemin);
            reader.ReadLine();

            int numero = 1;
            string? ligne;
            while ((ligne = reader.ReadLine()) != null)
            {
                numero++;
                try
                {
                    notes.Add(LireLigne(ligne, numero));
                }
                catch (NoteInvalideException e)
                {
                    Console.Error.WriteLine($"Ligne {e.Ligne} ignorée : {e.Message}");
                }
            }
        }
        // 6.5 Fichier absent
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Le fichier {chemin} est introuvable.");
        }

        return notes;
    }
}
