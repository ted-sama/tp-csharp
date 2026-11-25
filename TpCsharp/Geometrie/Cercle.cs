namespace Geometrie
{
    // 5.1.3 Cercle
    public class Cercle : Forme
    {
        public double Rayon { get; }

        public Cercle(double rayon) : base("Cercle")
        {
            if (rayon <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rayon), "Le rayon doit être strictement positif.");
            }
            Rayon = rayon;
        }

        public override double Aire()
        {
            return Math.PI * Rayon * Rayon;
        }

        public override double Perimetre()
        {
            return 2 * Math.PI * Rayon;
        }

        // 5.1.5 Decrire avec le rayon
        public override string Decrire()
        {
            return $"{base.Decrire()}, rayon = {Rayon:F2}";
        }
    }
}
