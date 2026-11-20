namespace Ecole;

public class NoteInvalideException : Exception
{
    public int Ligne { get; }

    public NoteInvalideException(string message, int ligne) : base(message)
    {
        Ligne = ligne;
    }
}
