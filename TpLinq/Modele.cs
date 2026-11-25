// 0.3 Les records
public record Note(string Nom, string Prenom, string Matiere, double Valeur, int Coef);
public record Inscrit(string Nom, string Prenom, string Promo);
// 3.3 Les bulletins
public record Bulletin(string Prenom, List<double> Valeurs);
