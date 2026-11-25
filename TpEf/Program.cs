using Microsoft.EntityFrameworkCore;

using (var db = new PromoContext())
{
    // Q7 et Q8 : on insère la promo seulement si la base est vide
    if (!db.Etudiants.Any())
    {
        var promo = Donnees.Promo();
        db.Etudiants.AddRange(promo);
        Console.WriteLine($"Avant : {db.Entry(promo[0]).State}");
        int n = db.SaveChanges();
        Console.WriteLine($"{n} lignes écrites");
        Console.WriteLine($"Après : {db.Entry(promo[0]).State}, Id = {promo[0].Id}");
    }
}

using (var db = new PromoContext())
{
    // Q9 : les étudiants de M2 triés par nom
    var m2 = db.Etudiants
        .Where(e => e.Promo == "M2")
        .OrderBy(e => e.Nom)
        .ToList();

    Console.WriteLine("Etudiants de M2 :");
    foreach (var e in m2)
        Console.WriteLine($"{e.Nom} {e.Prenom}");

    Console.WriteLine(db.Etudiants.Where(e => e.Promo == "M2").OrderBy(e => e.Nom).ToQueryString());

    // Q10 : le nombre de notes de chaque étudiant (sans Include, le Select suffit)
    var chaqueEtudiants = db.Etudiants
        .Select(e => new { e.Nom, e.Prenom, NbNotes = e.Notes.Count })
        .ToList();

    Console.WriteLine("Nombre de notes pour chaque étudiant :");
    foreach (var e in chaqueEtudiants)
        Console.WriteLine($"{e.Nom} {e.Prenom} : {e.NbNotes} notes");

    // Q11 : la moyenne de chaque matière
    var moyenneMatieres = db.Notes
        .GroupBy(n => n.Matiere)
        .Select(g => new { Matiere = g.Key, Moyenne = g.Average(n => n.Valeur) })
        .OrderBy(m => m.Matiere)
        .ToList();

    Console.WriteLine("Moyenne de chaque matière :");
    foreach (var m in moyenneMatieres)
        Console.WriteLine($"{m.Matiere} : {m.Moyenne:F2}");

    Console.WriteLine(db.Notes.GroupBy(n => n.Matiere).Select(g => new { Matiere = g.Key, Moyenne = g.Average(n => n.Valeur) }).OrderBy(m => m.Matiere).ToQueryString());
    // Le calcul est fait par la base (AVG et GROUP BY)
}

using (var db = new PromoContext())
{
    // Q12 : sans Include, les notes ne sont pas chargées donc on a 0 partout
    Console.WriteLine("Sans Include :");
    foreach (var e in db.Etudiants.ToList())
        Console.WriteLine($"{e.Nom} : {e.Notes.Count} notes");

    Console.WriteLine(db.Etudiants.ToQueryString());
}

using (var db = new PromoContext())
{
    // Nouveau contexte, pour ne pas être influencé par la première requête
    Console.WriteLine("Avec Include :");
    foreach (var e in db.Etudiants.Include(e => e.Notes).ToList())
        Console.WriteLine($"{e.Nom} : {e.Notes.Count} notes");

    Console.WriteLine(db.Etudiants.Include(e => e.Notes).ToQueryString());
}

using (var db = new PromoContext())
{
    // Q13 : la moyenne pondérée de chaque étudiant
    // Le Where évite de diviser par 0 pour les étudiants sans note
    var moyennePondereeEtudiants = db.Etudiants
        .Where(e => e.Notes.Any())
        .Select(e => new
        {
            e.Nom,
            e.Prenom,
            MoyennePonderee = e.Notes.Sum(n => n.Valeur * n.Coef) / e.Notes.Sum(n => n.Coef)
        })
        .OrderByDescending(e => e.MoyennePonderee)
        .ToList();

    Console.WriteLine("Moyenne pondérée de chaque étudiant :");
    foreach (var e in moyennePondereeEtudiants)
        Console.WriteLine($"{e.Nom} {e.Prenom} : {e.MoyennePonderee:F2}");
}

using (var db = new PromoContext())
{
    // Q14 : Alan Turing passe en M2
    var alan = db.Etudiants.FirstOrDefault(e => e.Nom == "Turing" && e.Prenom == "Alan");
    if (alan is not null)
    {
        Console.WriteLine($"Après chargement : {db.Entry(alan).State}");

        // Pas besoin d'Update : alan est suivi par le contexte, qui détecte le changement
        alan.Promo = "M2";
        Console.WriteLine($"Avant SaveChanges : {db.Entry(alan).State}");

        db.SaveChanges();
        Console.WriteLine($"Après SaveChanges : {db.Entry(alan).State}");
    }
}

using (var db = new PromoContext())
{
    // Q15 : suppression de Linus Torvalds
    var linus = db.Etudiants.FirstOrDefault(e => e.Nom == "Torvalds" && e.Prenom == "Linus");
    if (linus is not null)
    {
        // Ses notes sont supprimées en cascade par la base
        db.Etudiants.Remove(linus);
        db.SaveChanges();
    }

    Console.WriteLine($"Notes restantes : {db.Notes.Count()}");
}

using (var db = new PromoContext())
{
    // Q16 : le coefficient de toutes les notes de Réseau passe à 2
    // ExecuteUpdate s'exécute tout de suite, sans SaveChanges
    int lignesModifiees = db.Notes
        .Where(n => n.Matiere == "Reseau")
        .ExecuteUpdate(s => s.SetProperty(n => n.Coef, 2));

    Console.WriteLine($"{lignesModifiees} lignes modifiées");
}

using (var db = new PromoContext())
{
    // Q17 : après la migration AjoutEmail, les emails valent null car la colonne est annulable
    foreach (var e in db.Etudiants.ToList())
        Console.WriteLine($"{e.Nom} {e.Prenom} : {e.Email ?? "null"}");
}
