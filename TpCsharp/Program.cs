using Ecole;
using Geometrie;

// 1.3 Prénom et âge
// Console.Write("Entrez votre prénom : ");
// string name = Console.ReadLine() ?? "";

// Console.Write("Entrez votre année de naissance : ");
// int birthYear = int.Parse(Console.ReadLine() ?? "");

// int age = DateTime.Now.Year - birthYear;
// Console.WriteLine($"Bonjour, {name}! Vous avez {age} ans.");

// 2.1 Valeur ou référence : classe vs struct
// var c1 = new PointC { X = 1 };

// var c2 = c1;

// c2.X = 9;

// Console.WriteLine($"classe : c1.X = {c1.X}");

// var s1 = new PointS { X = 1 };

// var s2 = s1;

// s2.X = 9;

// Console.WriteLine($"struct : s1.X = {s1.X}");

// 2.1 Comparaison de chaînes
// string a = "info";

// string b = string.Concat("in", "fo");

// Console.WriteLine(a == b);

// 2.1 double vs decimal
// Console.WriteLine(0.1 + 0.2 == 0.3);

// Console.WriteLine(0.1m + 0.2m == 0.3m);

// 2.2.3 Correction avec string?, ?. et ?? puis avec is null
// NullControle.Run();

// 2.2.4 Âge entre 0 et 120 avec TryParse
// NullControle2.Run();

// 3.1 Tests de Mention avec une switch expression
// Console.WriteLine(ControleCollections.Mention(18.5));
// Console.WriteLine(ControleCollections.Mention(15.5));
// Console.WriteLine(ControleCollections.Mention(13.5));
// Console.WriteLine(ControleCollections.Mention(11.5));
// Console.WriteLine(ControleCollections.Mention(9.5));
// Console.WriteLine(ControleCollections.Mention(21)); // Exception

// 3.2 Moyenne, min, max d'une liste de notes
// List<double> notes = [12, 15.5, 9, 18];

// ControleCollections.CalculMoyenneEtMention(notes);

// 3.3 Dictionnaire étudiant -> notes
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

// 3.4 Clé absente puis TryGetValue
// List<double> notesInconnu = notesEtudiants["Inconnu"]; // KeyNotFoundException

// if (notesEtudiants.TryGetValue("Inconnu", out List<double>? notesTrouvees))
// {
//     Console.WriteLine($"Inconnu : {notesTrouvees.Count} notes");
// }
// else
// {
//     Console.WriteLine("Étudiant introuvable.");
// }

// 3.5 Decrire avec le filtrage par motif
// Console.WriteLine(ControleCollections.Decrire(4));
// Console.WriteLine(ControleCollections.Decrire(7));
// Console.WriteLine(ControleCollections.Decrire(""));
// Console.WriteLine(ControleCollections.Decrire("bonjour"));
// Console.WriteLine(ControleCollections.Decrire(new List<double> { 12, 15 }));
// Console.WriteLine(ControleCollections.Decrire(null));
// Console.WriteLine(ControleCollections.Decrire(3.14));

// 4.1 Tests de la classe Etudiant (les lignes // // ne compilent pas)
// var ada = new Etudiant("Lovelace", "Ada", 4)
// {
//     Naissance = new DateOnly(1815, 12, 10)
// };
// ada.AjouterNote(15);
// ada.AjouterNote(16);
// Console.WriteLine(ada);
// ada.Annee = 5;
// // ada.Nom = "Byron"; // error CS0200
// // ada.Naissance = new DateOnly(1816, 1, 1); // error CS8852
// // Console.WriteLine(ada._notes); // error CS0122

// 4.2 Tests de la classe Promo
// var alan = new Etudiant("Turing", "Alan", 2);
// alan.AjouterNote(9);
// alan.AjouterNote(12);

// var grace = new Etudiant("Hopper", "Grace", 5);
// grace.AjouterNote(17);
// grace.AjouterNote(18);

// Promo promo = new Promo("M1 Info");
// promo.Inscrire(ada);
// promo.Inscrire(alan);
// promo.Inscrire(grace);

// Console.WriteLine($"Recherche de Turing : {promo.Chercher("Turing")}");
// Console.WriteLine($"Major : {promo.Major()}");
// promo.AfficherReleve();

// 5.2 Polymorphisme : tri par aire et Dessiner
// List<Forme> formes = [new Cercle(1), new Rectangle(2, 5), new Carre(3), new Cercle(2.5)];
// formes.Sort();
// foreach (var f in formes)
// {
//     f.Dessiner();
// }

// 5.2 Aire totale et forme au plus grand périmètre
// double aireTotale = 0;
// Forme plusGrandPerimetre = formes[0];
// foreach (var forme in formes)
// {
//     aireTotale += forme.Aire();
//     if (forme.Perimetre() > plusGrandPerimetre.Perimetre())
//     {
//         plusGrandPerimetre = forme;
//     }
// }
// Console.WriteLine($"Aire totale : {aireTotale:F2}");
// Console.WriteLine($"Plus grand périmètre : {plusGrandPerimetre.Decrire()}");

// 5.3 Le piège du virtual (new vs override)
// Cercle c = new Cercle(1);
// Forme formeC = c;
// Console.WriteLine(c.Decrire());
// Console.WriteLine(formeC.Decrire());

// 6.1 Record Note : ==, ToString et with
var note1 = new Note("Lovelace", "Ada", "Algo", 15, 3);
var note2 = new Note("Lovelace", "Ada", "Algo", 15, 3);
Console.WriteLine(note1 == note2);
Console.WriteLine(note1);
var noteModifiee = note1 with { Valeur = 17 };
Console.WriteLine(noteModifiee);

// 6.1 == entre deux Etudiant
var ada1 = new Etudiant("Lovelace", "Ada", 4);
var ada2 = new Etudiant("Lovelace", "Ada", 4);
Console.WriteLine(ada1 == ada2);

// 6.4 / 6.5 Lecture de notes.csv (lignes invalides et fichier absent gérés dans LecteurNotes)
List<Note> notesFichier = LecteurNotes.Lire("notes.csv");

// 6.6 Promo remplie avec les notes valides puis relevé
Promo promoFichier = new Promo("M1 Info");
foreach (var note in notesFichier)
{
    Etudiant? etudiant = promoFichier.Chercher(note.Nom);
    if (etudiant is null)
    {
        etudiant = new Etudiant(note.Nom, note.Prenom, 4);
        promoFichier.Inscrire(etudiant);
    }
    etudiant.AjouterNote(note.Valeur);
}
promoFichier.AfficherReleve();
