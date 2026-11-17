using Ecole;

// Console.Write("Entrez votre prénom : ");
// string name = Console.ReadLine() ?? "";

// Console.Write("Entrez votre année de naissance : ");
// int birthYear = int.Parse(Console.ReadLine() ?? "");

// int age = DateTime.Now.Year - birthYear;
// Console.WriteLine($"Bonjour, {name}! Vous avez {age} ans.");

// var c1 = new PointC { X = 1 };

// var c2 = c1;

// c2.X = 9;

// Console.WriteLine($"classe : c1.X = {c1.X}");

// var s1 = new PointS { X = 1 };

// var s2 = s1;

// s2.X = 9;

// Console.WriteLine($"struct : s1.X = {s1.X}");

// string a = "info";

// string b = string.Concat("in", "fo");

// Console.WriteLine(a == b);

// Console.WriteLine(0.1 + 0.2 == 0.3);

// Console.WriteLine(0.1m + 0.2m == 0.3m);

// NullControle.Run();

// NullControle2.Run();

// Console.WriteLine(ControleCollections.Mention(18.5));
// Console.WriteLine(ControleCollections.Mention(15.5));
// Console.WriteLine(ControleCollections.Mention(13.5));
// Console.WriteLine(ControleCollections.Mention(11.5));
// Console.WriteLine(ControleCollections.Mention(9.5));
// Console.WriteLine(ControleCollections.Mention(21)); // Exception

// List<double> notes = [12, 15.5, 9, 18];

// ControleCollections.CalculMoyenneEtMention(notes);

// Dictionary<string, List<double>> notesEtudiants = new Dictionary<string, List<double>>
// {
//     { "Scott", new List<double> { 13, 15.5, 14, 17 } },
//     { "Luther", new List<double> { 5, 10, 15, 17 } },
//     { "Zoey", new List<double> { 20, 15, 13, 18 } }
// };

// notesEtudiants["Luther"].Add(12);

// foreach (var (nom, notes) in notesEtudiants)
// {
//     double moyenne = ControleCollections.Moyenne(notes);
//     Console.WriteLine($"{nom} : {notes.Count} notes, moyenne {moyenne:F2}, mention {ControleCollections.Mention(moyenne)}");
// }

// List<double> notesInconnu = notesEtudiants["Inconnu"]; // KeyNotFoundException

// if (notesEtudiants.TryGetValue("Inconnu", out List<double>? notesTrouvees))
// {
//     Console.WriteLine($"Inconnu : {notesTrouvees.Count} notes");
// }
// else
// {
//     Console.WriteLine("Étudiant introuvable.");
// }

// Console.WriteLine(ControleCollections.Decrire(4));
// Console.WriteLine(ControleCollections.Decrire(7));
// Console.WriteLine(ControleCollections.Decrire(""));
// Console.WriteLine(ControleCollections.Decrire("bonjour"));
// Console.WriteLine(ControleCollections.Decrire(new List<double> { 12, 15 }));
// Console.WriteLine(ControleCollections.Decrire(null));
// Console.WriteLine(ControleCollections.Decrire(3.14));

var ada = new Etudiant("Lovelace", "Ada", 4)
{
    Naissance = new DateOnly(1815, 12, 10)
};
ada.AjouterNote(15);
ada.AjouterNote(16);
Console.WriteLine(ada);
ada.Annee = 5;
// ada.Nom = "Byron"; // error CS0200
// ada.Naissance = new DateOnly(1816, 1, 1); // error CS8852
// Console.WriteLine(ada._notes); // error CS0122

var alan = new Etudiant("Turing", "Alan", 2);
alan.AjouterNote(9);
alan.AjouterNote(12);

var grace = new Etudiant("Hopper", "Grace", 5);
grace.AjouterNote(17);
grace.AjouterNote(18);

Promo promo = new Promo("M1 Info");
promo.Inscrire(ada);
promo.Inscrire(alan);
promo.Inscrire(grace);

Console.WriteLine($"Recherche de Turing : {promo.Chercher("Turing")}");
Console.WriteLine($"Major : {promo.Major()}");
promo.AfficherReleve();
