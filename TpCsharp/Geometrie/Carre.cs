namespace Geometrie
{
    // 5.1.4 Carre (sealed)
    public sealed class Carre : Rectangle
    {
        public Carre(double cote) : base(cote, cote, "Carré")
        {
        }
    }

    // 5.1.4 Essai de dérivation
    // error CS0509: 'CarreMagique' : dérivation du type sealed 'Carre' impossible
    // class CarreMagique : Carre
    // {
    //     public CarreMagique(double cote) : base(cote) { }
    // }
}
