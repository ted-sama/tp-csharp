static class NullControle
{
    public static void Run()
    {
        string? saisie = Console.ReadLine();
        Console.WriteLine($"Vous avez tapé {saisie?.Length ?? 0} caractères");

        string? saisieAvecTest = Console.ReadLine();

        if (saisieAvecTest is null)
        {
            Console.WriteLine("Aucune saisie.");
        }
        else
        {
            Console.WriteLine($"Vous avez tapé {saisieAvecTest.Length} caractères");
        }
    }
}
