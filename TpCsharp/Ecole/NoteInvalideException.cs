namespace Ecole;

// 6.2 Exception avec le numéro de ligne
public class NoteInvalideException : Exception
{
    public int Ligne { get; }

    public NoteInvalideException(string message, int ligne) : base(message)
    {
        Ligne = ligne;
    }
}
