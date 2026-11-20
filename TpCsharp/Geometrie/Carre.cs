namespace Geometrie
{
    public sealed class Carre : Rectangle
    {
        public Carre(double cote) : base(cote, cote, "Carré")
        {
        }
    }

    // error CS0509: 'CarreMagique' : dérivation du type sealed 'Carre' impossible
    // class CarreMagique : Carre
    // {
    //     public CarreMagique(double cote) : base(cote) { }
    // }
}
