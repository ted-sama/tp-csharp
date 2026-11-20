namespace Geometrie
{
    public class Rectangle : Forme
    {
        public double Largeur { get; }
        public double Hauteur { get; }

        public Rectangle(double largeur, double hauteur) : this(largeur, hauteur, "Rectangle")
        {
        }

        protected Rectangle(double largeur, double hauteur, string nom) : base(nom)
        {
            if (largeur <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(largeur), "La largeur doit être strictement positive.");
            }
            if (hauteur <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hauteur), "La hauteur doit être strictement positive.");
            }
            Largeur = largeur;
            Hauteur = hauteur;
        }

        public override double Aire()
        {
            return Largeur * Hauteur;
        }

        public override double Perimetre()
        {
            return 2 * (Largeur + Hauteur);
        }
    }
}
