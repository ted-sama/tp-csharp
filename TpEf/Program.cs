using Microsoft.EntityFrameworkCore;

using (var db = new PromoContext())
{
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
    // Les étudiants de M2 triés par nom
    var m2 = db.Etudiants
        .Where(e => e.Promo == "M2")
        .OrderBy(e => e.Nom)
        .ToList();

    // Affichage des étudiants de M2
    Console.WriteLine("Etudiants de M2 :");
    foreach (var e in m2)
        Console.WriteLine($"{e.Nom} {e.Prenom}");
    
    // Affichage de la requête SQL générée par EF Core
    Console.WriteLine(db.Etudiants.Where(e => e.Promo == "M2").OrderBy(e => e.Nom).ToQueryString());



    // Pour chaque étudiant, son nom et son nombre de notes, avec une projection Select vers un type anonyme (sans Include).
    var chaqueEtudiants = db.Etudiants
        .Select(e => new { e.Nom, e.Prenom, NbNotes = e.Notes.Count })
        .ToList();

    // Affichage du nombre de notes pour chaque étudiant
    Console.WriteLine("Nombre de notes pour chaque étudiant :");
    foreach (var e in chaqueEtudiants)
        Console.WriteLine($"{e.Nom} {e.Prenom} : {e.NbNotes} notes");
    

    // La moyenne de chaque matière
    var moyenneMatieres = db.Notes
        .GroupBy(n => n.Matiere)
        .Select(g => new { Matiere = g.Key, Moyenne = g.Average(n => n.Valeur) })
        .OrderBy(m => m.Matiere)
        .ToList();

    // Affichage de la moyenne de chaque matière
    Console.WriteLine("Moyenne de chaque matière :");
    foreach (var m in moyenneMatieres)
        Console.WriteLine($"{m.Matiere} : {m.Moyenne:F2}");
    
    // Affichage de la requête SQL générée par EF Core pour la moyenne de chaque matière
    Console.WriteLine(db.Notes.GroupBy(n => n.Matiere).Select(g => new { Matiere = g.Key, Moyenne = g.Average(n => n.Valeur) }).OrderBy(m => m.Matiere).ToQueryString());
    // En SQL, le calcul est fait dans le SELECT



}

using (var db = new PromoContext())
{
    Console.WriteLine("Sans Include :");
    foreach (var e in db.Etudiants.ToList())
        Console.WriteLine($"{e.Nom} : {e.Notes.Count} notes");

    // Affichage de la requête SQL générée par EF Core pour les étudiants sans Include
    Console.WriteLine(db.Etudiants.ToQueryString());
}

using (var db = new PromoContext())
{
    Console.WriteLine("Avec Include :");
    foreach (var e in db.Etudiants.Include(e => e.Notes).ToList())
        Console.WriteLine($"{e.Nom} : {e.Notes.Count} notes");

    // Affichage de la requête SQL générée par EF Core pour les étudiants avec Include
    Console.WriteLine(db.Etudiants.Include(e => e.Notes).ToQueryString());
    // Include gère la jointure et le chargement des notes pour chaque étudiant côté SQL
}

using (var db = new PromoContext())
{
    // La moyenne pondérée de chaque étudiant qui a au moins une note (somme des valeurs ×
    // coefficients, divisée par la somme des coefficients), de la meilleure à la moins bonne, affichée avec
    // deux décimales.

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

    // Affichage de la moyenne pondérée de chaque étudiant
    Console.WriteLine("Moyenne pondérée de chaque étudiant :");
    foreach (var e in moyennePondereeEtudiants)
        Console.WriteLine($"{e.Nom} {e.Prenom} : {e.MoyennePonderee:F2}");
}

using (var db = new PromoContext())
{
    // Alan Turing passe en M2
    var alan = db.Etudiants.FirstOrDefault(e => e.Nom == "Turing" && e.Prenom == "Alan");
    if (alan is not null)
    {
        Console.WriteLine($"Après chargement : {db.Entry(alan).State}");

        alan.Promo = "M2";
        Console.WriteLine($"Avant SaveChanges : {db.Entry(alan).State}");

        db.SaveChanges();
        Console.WriteLine($"Après SaveChanges : {db.Entry(alan).State}");
    }
}

using (var db = new PromoContext())
{
    // Suppression de Linus Torvalds (absent dès le deuxième lancement)
    var linus = db.Etudiants.FirstOrDefault(e => e.Nom == "Torvalds" && e.Prenom == "Linus");
    if (linus is not null)
    {
        db.Etudiants.Remove(linus);
        db.SaveChanges();
    }

    Console.WriteLine($"Notes restantes : {db.Notes.Count()}");
}

using (var db = new PromoContext())
{
    // Le coefficient de toutes les notes de Réseau passe à 2
    int lignesModifiees = db.Notes
        .Where(n => n.Matiere == "Reseau")
        .ExecuteUpdate(s => s.SetProperty(n => n.Coef, 2));

    Console.WriteLine($"{lignesModifiees} lignes modifiées");
}

using (var db = new PromoContext())
{
    // Les étudiants et leur email après la migration AjoutEmail
    foreach (var e in db.Etudiants.ToList())
        Console.WriteLine($"{e.Nom} {e.Prenom} : {e.Email ?? "null"}");
}
