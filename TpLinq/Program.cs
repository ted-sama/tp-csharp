using System.Globalization;

// 0.3 Parser une ligne
static Note? Parser(string ligne)
{
    var c = ligne.Split(',');
    if (c.Length != 5) return null;
    if (!double.TryParse(c[3], CultureInfo.InvariantCulture, out var v)) return null;
    if (v < 0 || v > 20) return null;
    if (!int.TryParse(c[4], out var coef)) return null;
    return new Note(c[0], c[1], c[2], v, coef);
}

// 0.3.1 / 0.3.2 Lecture des notes (Where garde le type Note?, OfType donne directement Note)
// IEnumerable<Note?> avecWhere = File.ReadLines("notes.csv").Skip(1).Select(Parser).Where(n => n is not null);

List<Note> notes = File.ReadLines("notes.csv")
    .Skip(1)
    .Select(Parser)
    .OfType<Note>()
    .ToList();

// 0.3.3 Lecture des inscrits
// List<Inscrit> inscrits = File.ReadLines("etudiants.csv")
//     .Skip(1)
//     .Select(ligne => ligne.Split(','))
//     .Select(c => new Inscrit(c[0], c[1], c[2]))
//     .ToList();

// 0.3.4 Bonus : numéros des lignes invalides
// IEnumerable<int> lignesInvalides = File.ReadLines("notes.csv")
//     .Skip(1)
//     .Select((ligne, i) => (Numero: i + 2, Note: Parser(ligne)))
//     .Where(l => l.Note is null)
//     .Select(l => l.Numero);

// foreach (int numero in lignesInvalides)
// {
//     Console.Error.WriteLine($"Ligne {numero} invalide");
// }

// Console.WriteLine($"{notes.Count} notes valides et {inscrits.Count} inscrits");

// 1.1.1 Filtre générique (le seul endroit avec une boucle pour calculer)
// static List<Note> Filtrer(List<Note> source, Func<Note, bool> critere)
// {
//     List<Note> resultat = new List<Note>();
//     foreach (Note note in source)
//     {
//         if (critere(note))
//         {
//             resultat.Add(note);
//         }
//     }
//     return resultat;
// }

// 1.1.2 Critère sous forme de méthode nommée
// static bool EstEnAlgo(Note n)
// {
//     return n.Matiere == "Algo";
// }

// 1.1.2 Filtrer avec une méthode nommée
// List<Note> notesAlgo = Filtrer(notes, EstEnAlgo);
// Console.WriteLine($"{notesAlgo.Count} notes en Algo");
// foreach (Note note in notesAlgo)
// {
//     Console.WriteLine(note);
// }

// 1.1.2 Filtrer avec une lambda : notes sous 10
// List<Note> notesSous10 = Filtrer(notes, n => n.Valeur < 10);
// Console.WriteLine($"{notesSous10.Count} notes sous 10");
// foreach (Note note in notesSous10)
// {
//     Console.WriteLine(note);
// }

// 1.1.2 Filtrer avec une lambda : notes d'Ada >= 15
// List<Note> bonnesNotesAda = Filtrer(notes, n => n.Prenom == "Ada" && n.Valeur >= 15);
// Console.WriteLine($"{bonnesNotesAda.Count} bonnes notes d'Ada");
// foreach (Note note in bonnesNotesAda)
// {
//     Console.WriteLine(note);
// }

// 1.1.3 La lambda dans une variable
// Func<Note, bool> critereAda = n => n.Prenom == "Ada" && n.Valeur >= 15;
// Console.WriteLine($"{Filtrer(notes, critereAda).Count} bonnes notes d'Ada (avec la variable)");
// // var lambdaAda = n => n.Prenom == "Ada" && n.Valeur >= 15; // error CS8917

// 1.2.1 Func : points pondérés
// Func<Note, double> PointsPonderes = n => n.Valeur * n.Coef;
// foreach (Note note in notes)
// {
//     Console.WriteLine($"{note.Nom} {note.Prenom}, {note.Matiere}, Note: {note.Valeur} Coef: {note.Coef} => {PointsPonderes(note)}");
// }

// 1.2.1 Action : affichage "Ada · Algo · 15"
// Action<Note> afficher = n => Console.WriteLine($"{n.Prenom} · {n.Matiere} · {n.Valeur}");
// foreach (Note note in notes)
// {
//     afficher(note);
// }

// 1.2.2 Méthode qui renvoie une lambda
// static Func<Note, bool> AuDessusDe(double seuil) => n => n.Valeur > seuil;

// 1.2.2 Notes au dessus de 14 avec AuDessusDe
// List<Note> notesAuDessusDe14 = Filtrer(notes, AuDessusDe(14));
// Console.WriteLine($"{notesAuDessusDe14.Count} notes au dessus de 14");
// foreach (Note note in notesAuDessusDe14)
// {
//     Console.WriteLine($"{note.Nom} {note.Prenom}, {note.Matiere}, Note: {note.Valeur} Coef: {note.Coef}");
// }

// 1.2.3 Tri d'une copie par matière puis note décroissante
// List<Note> copieTriee = new List<Note>(notes);
// copieTriee.Sort((n1, n2) =>
// {
//     int parMatiere = n1.Matiere.CompareTo(n2.Matiere);
//     return parMatiere != 0 ? parMatiere : n2.Valeur.CompareTo(n1.Valeur);
// });
// Console.WriteLine("Copie triée par matière puis note décroissante");
// foreach (Note note in copieTriee)
// {
//     Console.WriteLine($"{note.Nom} {note.Prenom}, {note.Matiere}, Note: {note.Valeur} Coef: {note.Coef}");
// }

// 1.2.3 Suppression des notes de Reseau dans la copie
// int nbSupprimees = copieTriee.RemoveAll(n => n.Matiere == "Reseau");
// Console.WriteLine($"{nbSupprimees} notes de Reseau supprimées de la copie");
// foreach (Note note in copieTriee)
// {
//     Console.WriteLine($"{note.Nom} {note.Prenom}, {note.Matiere}, Note: {note.Valeur} Coef: {note.Coef}");
// }

// 1.3 La capture de variables
// double seuil = 12;
// Func<Note, bool> admis = n => n.Valeur >= seuil;
// Console.WriteLine(Filtrer(notes, admis).Count);
// seuil = 15;
// Console.WriteLine(Filtrer(notes, admis).Count);

// 1.3 Garder le seuil 12 : on copie la valeur dans une variable qui ne change plus
// double seuilFixe = 12;
// Func<Note, bool> admis12 = n => n.Valeur >= seuilFixe;
// seuil = 18;
// Console.WriteLine(Filtrer(notes, admis12).Count);

// 2.1.1 Notes d'Algo par valeur décroissante
// List<Note> algoParValeur = notes
//     .Where(n => n.Matiere == "Algo")
//     .OrderByDescending(n => n.Valeur)
//     .ToList();
// Console.WriteLine("Notes d'Algo par valeur décroissante");
// foreach (Note note in algoParValeur)
// {
//     Console.WriteLine($"{note.Nom} {note.Prenom}, {note.Matiere}, Note: {note.Valeur} Coef: {note.Coef}");
// }

// 2.1.2 Liste des matières sans doublons et triée
// List<string> matieres = notes
//     .Select(n => n.Matiere)
//     .Distinct()
//     .OrderBy(m => m)
//     .ToList();
// Console.WriteLine("Liste des matières sans doublons et triée");
// foreach (string matiere in matieres)
// {
//     Console.WriteLine(matiere);
// }

// 2.1.3 Prénom et valeur des notes sous 10, en type anonyme
// var sous10Anonyme = notes
//     .Where(n => n.Valeur < 10)
//     .Select(n => new { n.Prenom, n.Valeur })
//     .ToList();
// foreach (var s in sous10Anonyme)
// {
//     Console.WriteLine($"{s.Prenom} : {s.Valeur}");
// }

// 2.1.3 Pareil avec un tuple
// List<(string Prenom, double Valeur)> sous10Tuple = notes
//     .Where(n => n.Valeur < 10)
//     .Select(n => (n.Prenom, n.Valeur))
//     .ToList();
// foreach (var s in sous10Tuple)
// {
//     Console.WriteLine($"{s.Prenom} : {s.Valeur}");
// }

// 2.1.4 Classement avec deux OrderBy (le 2e écrase le 1er)
// List<Note> classementFaux = notes
//     .OrderBy(n => n.Matiere)
//     .OrderByDescending(n => n.Valeur)
//     .ToList();
// Console.WriteLine("Classement avec deux OrderBy");
// foreach (Note note in classementFaux)
// {
//     Console.WriteLine($"{note.Prenom}, {note.Matiere}, Note: {note.Valeur}");
// }

// 2.1.4 Classement corrigé avec ThenBy
// List<Note> classement = notes
//     .OrderBy(n => n.Matiere)
//     .ThenByDescending(n => n.Valeur)
//     .ThenBy(n => n.Prenom)
//     .ToList();
// Console.WriteLine("Classement avec ThenBy");
// foreach (Note note in classement)
// {
//     Console.WriteLine($"{note.Prenom}, {note.Matiere}, Note: {note.Valeur}");
// }

// 2.1.5 Les 3 meilleures notes
// List<Note> top3 = notes
//     .OrderByDescending(n => n.Valeur)
//     .Take(3)
//     .ToList();
// Console.WriteLine("Top 3");
// foreach (Note note in top3)
// {
//     Console.WriteLine($"{note.Prenom}, {note.Matiere}, Note: {note.Valeur}");
// }

// 2.1.5 Les notes de la 4e à la 6e place
// List<Note> places4a6 = notes
//     .OrderByDescending(n => n.Valeur)
//     .Skip(3)
//     .Take(3)
//     .ToList();
// Console.WriteLine("Places 4 à 6");
// foreach (Note note in places4a6)
// {
//     Console.WriteLine($"{note.Prenom}, {note.Matiere}, Note: {note.Valeur}");
// }

// 2.2.1 Nombre de notes, nombre sous 10, moyenne simple
// int nbNotes = notes.Count();
// int nbSous10 = notes.Count(n => n.Valeur < 10);
// double moyenneSimple = notes.Average(n => n.Valeur);
// Console.WriteLine($"{nbNotes} notes, {nbSous10} sous 10, moyenne simple {moyenneSimple:F2}");

// 2.2.2 Max et MaxBy
// double meilleureNote = notes.Max(n => n.Valeur);
// Note? noteMax = notes.MaxBy(n => n.Valeur);
// Console.WriteLine($"Max : {meilleureNote}");
// Console.WriteLine($"MaxBy : {noteMax}");

// 2.2.3 Moyenne pondérée de toutes les notes
// double moyennePonderee = notes.Sum(n => n.Valeur * n.Coef) / notes.Sum(n => n.Coef);
// Console.WriteLine($"Moyenne pondérée : {moyennePonderee:F2}");

// 2.2.4 Prénoms distincts avec Aggregate
// string prenomsAggregate = notes
//     .Select(n => n.Prenom)
//     .Distinct()
//     .Aggregate((a, b) => a + ", " + b);
// Console.WriteLine(prenomsAggregate);

// 2.2.4 Prénoms distincts avec string.Join
// string prenomsJoin = string.Join(", ", notes.Select(n => n.Prenom).Distinct());
// Console.WriteLine(prenomsJoin);

// 2.3.1 Note de Linus en BD avec FirstOrDefault
// Note? linusBD = notes.FirstOrDefault(n => n.Prenom == "Linus" && n.Matiere == "BD");
// Console.WriteLine($"Linus en BD : {linusBD?.ToString() ?? "null"}");
// // Note linusBDFirst = notes.First(n => n.Prenom == "Linus" && n.Matiere == "BD"); // InvalidOperationException

// 2.3.2 Note d'Ada en Algo avec Single
// Note adaAlgo = notes.Single(n => n.Prenom == "Ada" && n.Matiere == "Algo");
// Console.WriteLine($"Ada en Algo : {adaAlgo}");
// // Note adaSingle = notes.Single(n => n.Prenom == "Ada"); // InvalidOperationException

// 2.3.3 Any et All
// bool uneNoteSous10 = notes.Any(n => n.Valeur < 10);
// bool tousAuMoins12EnBD = notes.Where(n => n.Matiere == "BD").All(n => n.Valeur >= 12);
// bool allSurVide = new List<Note>().All(n => n.Valeur > 100);
// Console.WriteLine($"Une note sous 10 : {uneNoteSous10}");
// Console.WriteLine($"Tous au moins 12 en BD : {tousAuMoins12EnBD}");
// Console.WriteLine($"All sur une liste vide : {allSurVide}");

// 3.1.1 Notes groupées par matière
// List<IGrouping<string, Note>> groupesMatiere = notes.GroupBy(n => n.Matiere).ToList();
// foreach (IGrouping<string, Note> groupe in groupesMatiere)
// {
//     Console.WriteLine(groupe.Key);
//     foreach (Note note in groupe)
//     {
//         Console.WriteLine($"  {note.Prenom} : {note.Valeur}");
//     }
// }

// 3.1.2 Statistiques par matière, par moyenne décroissante
// var statsMatieres = notes
//     .GroupBy(n => n.Matiere)
//     .Select(g => new
//     {
//         Matiere = g.Key,
//         Nombre = g.Count(),
//         Moyenne = g.Average(n => n.Valeur),
//         Min = g.Min(n => n.Valeur),
//         Max = g.Max(n => n.Valeur),
//         Major = g.MaxBy(n => n.Valeur)!.Prenom
//     })
//     .OrderByDescending(s => s.Moyenne)
//     .ToList();
// foreach (var s in statsMatieres)
// {
//     Console.WriteLine($"{s.Matiere} : {s.Nombre} notes, moyenne {s.Moyenne:F2}, min {s.Min}, max {s.Max}, major {s.Major}");
// }

// 3.1.3 Moyenne pondérée par étudiant dans un dictionnaire
// Dictionary<string, double> moyennesEtudiants = notes
//     .GroupBy(n => n.Prenom)
//     .ToDictionary(g => g.Key, g => g.Sum(n => n.Valeur * n.Coef) / g.Sum(n => n.Coef));
// foreach (var (prenom, moyenne) in moyennesEtudiants.OrderByDescending(m => m.Value))
// {
//     Console.WriteLine($"{prenom} : {moyenne:F2}");
// }

// 3.1.4 Nombre de notes par étudiant avec CountBy
// foreach (var (prenom, nombre) in notes.CountBy(n => n.Prenom))
// {
//     Console.WriteLine($"{prenom} : {nombre} notes");
// }

// 3.1.4 Pareil avec GroupBy et Count
// var nbNotesParEtudiant = notes
//     .GroupBy(n => n.Prenom)
//     .Select(g => new { Prenom = g.Key, Nombre = g.Count() })
//     .ToList();
// foreach (var e in nbNotesParEtudiant)
// {
//     Console.WriteLine($"{e.Prenom} : {e.Nombre} notes");
// }

// 3.2.1 Jointure notes / inscrits sur nom et prénom
// var releve = notes
//     .Join(inscrits,
//         n => new { n.Nom, n.Prenom },
//         ins => new { ins.Nom, ins.Prenom },
//         (n, ins) => new { n.Prenom, ins.Promo, n.Matiere, n.Valeur })
//     .ToList();
// foreach (var r in releve)
// {
//     Console.WriteLine($"{r.Prenom}, {r.Promo}, {r.Matiere}, {r.Valeur}");
// }

// 3.2.2 Moyenne simple par promo
// var moyennesPromo = releve
//     .GroupBy(r => r.Promo)
//     .Select(g => new { Promo = g.Key, Moyenne = g.Average(r => r.Valeur) })
//     .ToList();
// foreach (var mp in moyennesPromo)
// {
//     Console.WriteLine($"{mp.Promo} : {mp.Moyenne:F2}");
// }

// 3.2.3 Inscrits sans note avec Where et Any
// List<Inscrit> sansNote = inscrits
//     .Where(ins => !notes.Any(n => n.Nom == ins.Nom && n.Prenom == ins.Prenom))
//     .ToList();
// foreach (Inscrit ins in sansNote)
// {
//     Console.WriteLine($"Sans note : {ins.Prenom} {ins.Nom}");
// }

// 3.2.3 Pareil avec GroupJoin
// List<Inscrit> sansNoteGroupJoin = inscrits
//     .GroupJoin(notes,
//         ins => new { ins.Nom, ins.Prenom },
//         n => new { n.Nom, n.Prenom },
//         (ins, notesInscrit) => new { Inscrit = ins, NbNotes = notesInscrit.Count() })
//     .Where(x => x.NbNotes == 0)
//     .Select(x => x.Inscrit)
//     .ToList();
// foreach (Inscrit ins in sansNoteGroupJoin)
// {
//     Console.WriteLine($"Sans note (GroupJoin) : {ins.Prenom} {ins.Nom}");
// }

// 3.2.4 Index prénom -> notes avec ToLookup
// ILookup<string, Note> index = notes.ToLookup(n => n.Prenom);
// foreach (Note note in index["Grace"])
// {
//     Console.WriteLine($"Grace, {note.Matiere} : {note.Valeur}");
// }
// Console.WriteLine($"Prénom absent : {index["Inconnu"].Count()} notes");

// 3.3.1 Bulletins à partir des notes
// List<Bulletin> bulletins = notes
//     .GroupBy(n => n.Prenom)
//     .Select(g => new Bulletin(g.Key, g.Select(n => n.Valeur).ToList()))
//     .ToList();
// foreach (Bulletin bulletin in bulletins)
// {
//     Console.WriteLine($"{bulletin.Prenom} : {string.Join(", ", bulletin.Valeurs)}");
// }

// 3.3.2 Toutes les valeurs à plat avec SelectMany (Select donnerait une List<List<double>>)
// List<double> toutesValeurs = bulletins.SelectMany(b => b.Valeurs).ToList();
// Console.WriteLine(string.Join(", ", toutesValeurs));

// 3.3.3 Paires (prénom, valeur) avec la 2e forme de SelectMany
// List<(string Prenom, double Valeur)> paires = bulletins
//     .SelectMany(b => b.Valeurs, (b, v) => (b.Prenom, v))
//     .ToList();
// foreach (var paire in paires)
// {
//     Console.WriteLine($"({paire.Prenom}, {paire.Valeur})");
// }

// 4.1 Une requête est recalculée à chaque parcours (sur une copie pour ne pas ajouter Margaret dans notes)
// List<Note> notes4 = new List<Note>(notes);
// var bonnes = notes4.Where(n => n.Valeur >= 15);
// Console.WriteLine(bonnes.Count());
// notes4.Add(new Note("Hamilton", "Margaret", "Algo", 19, 3));
// Console.WriteLine(bonnes.Count());
// var figees = notes4.Where(n => n.Valeur >= 15).ToList();
// notes4.Add(new Note("Hamilton", "Margaret", "BD", 20, 2));
// Console.WriteLine(bonnes.Count() + " / " + figees.Count);

// 4.2 à 4.4 Expérience avec un effet de bord dans le Where (retirée après, résultats dans REPONSES.txt)
// // var avecTrace = notes.Where(n =>
// // {
// //     Console.WriteLine($"test de {n.Prenom} {n.Matiere}");
// //     return n.Valeur >= 15;
// // });
// // Console.WriteLine(avecTrace.Count());
// // Console.WriteLine(avecTrace.Count());
// // List<Note> avecTraceListe = avecTrace.ToList();
// // Console.WriteLine(avecTrace.Count() > 0);
// // Console.WriteLine(avecTrace.Any());

// 5.1 Utilisation de la classe Promo
Promo promo = new Promo("M1 Info", notes);
Console.WriteLine($"Étudiants : {string.Join(", ", promo.Etudiants())}");
Console.WriteLine($"Moyenne d'Ada : {promo.Moyenne("Ada"):F2}");
Console.WriteLine($"Moyenne de Margaret : {promo.Moyenne("Margaret"):F2}");
Console.WriteLine($"Moyenne générale : {promo.MoyenneGenerale():F2}");
Console.WriteLine($"Major : {promo.Major()}");
foreach (var (prenom, nombre) in promo.NbAdmisParMatiere())
{
    Console.WriteLine($"{prenom} : {nombre} admis");
}

// 5.2 Relevé dans l'ordre du classement
foreach (string ligne in promo.Releve())
{
    Console.WriteLine(ligne);
}
