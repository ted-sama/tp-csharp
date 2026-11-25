// 2.2.3 Le null sous contrôle
static class NullControle
{
    public static void Run()
    {
        // Avec ?. et ??
        string? saisie = Console.ReadLine();
        Console.WriteLine($"Vous avez tapé {saisie?.Length ?? 0} caractères");

        // Avec is null
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
