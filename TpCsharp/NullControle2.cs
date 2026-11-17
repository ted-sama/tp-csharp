static class NullControle2
{
    public static void Run()
    {
        int saisie;

        while (!int.TryParse(Console.ReadLine(), out saisie) || saisie < 0 || saisie > 120)
        {
            Console.WriteLine("Invalide");
        }

        Console.WriteLine($"Vous avez tapé {saisie}");
    }
}
