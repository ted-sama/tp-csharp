// 1.1 (2, 3) Note toujours valide : valeur entre 0 et 20, coef > 0
public record Note(string Nom, string Prenom, string Matiere, double Valeur, int Coef)
{
    public double Valeur { get; init; } = Valeur is >= 0 && Valeur <= 20 
        ? Valeur
        : throw new ArgumentOutOfRangeException(nameof(Valeur), "La valeur doit être comprise entre 0 et 20");

    public int Coef { get; init; } = VerifierCoef(Coef);

    private static int VerifierCoef(int coef)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(coef);
        return coef;
    }
}

// 0.1 Inscrit avec le code postal
public record Inscrit(string Nom, string Prenom, string Promo, string CodePostal);

// 4.1 (17) Commune
public record Commune(string Nom, string Code, int Population)
{
    public int Population { get; init; } = Population is >= 0
        ? Population
        : throw new ArgumentOutOfRangeException(nameof(Population), "La population doit être positive");
}