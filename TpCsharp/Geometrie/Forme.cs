namespace Geometrie
{
    // 5.1.2 Classe abstraite Forme
    public abstract class Forme : IDessinable, IComparable<Forme>
    {
        public string Nom { get; }

        protected Forme(string nom)
        {
            if (string.IsNullOrWhiteSpace(nom))
            {
                throw new ArgumentException("Le nom ne peut pas être vide ou null.", nameof(nom));
            }
            Nom = nom;
        }

        public abstract double Aire();

        public abstract double Perimetre();

        public virtual string Decrire()
        {
            return $"{Nom} : aire = {Aire():F2}, périmètre = {Perimetre():F2}";
        }

        public int CompareTo(Forme? autre)
        {
            if (autre is null)
            {
                return 1;
            }
            return Aire().CompareTo(autre.Aire());
        }

        public void Dessiner()
        {
            Console.WriteLine(Decrire());
        }
    }
}
