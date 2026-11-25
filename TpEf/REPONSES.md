# TP 4 : Entity Framework Core

## 1. Le modèle et la base

6. Deux tables sont créées, Etudiants et Notes. La clé étrangère est FK_Notes_Etudiants_EtudiantId sur Notes.EtudiantId, avec un index IX_Notes_EtudiantId. Comme elle est en Cascade, supprimer un étudiant supprime aussi ses notes.

### Questions

- Car Nullable est activé dans le projet et que Nom et Matiere sont des `string` et pas des `string?`. EF Core en déduit que les colonnes sont obligatoires.
- C'est une convention de nommage : EtudiantId correspond à la navigation Etudiant suivie du nom de sa clé, Id. EF Core fait donc le lien tout seul.

## 2. Insérer la promo

7. L'état est Added avant SaveChanges, 19 lignes sont écrites (6 étudiants et 13 notes), puis l'état passe à Unchanged et l'Id vaut 1.

8. Rien n'est inséré, car les étudiants sont déjà dans promo.db et Any() renvoie vrai. Dans le journal on voit juste un SELECT EXISTS.

## 3. Interroger la base

9. Hamilton Margaret, Hopper Grace et Liskov Barbara.

10. Lovelace 3, Turing 3, Hopper 3, Torvalds 2, Liskov 2, Hamilton 0. Les notes sont comptées dans une sous-requête SQL.

11. Algo 13,25, BD 14,50 et Reseau 13,60. Le calcul est fait par la base avec AVG et GROUP BY, seules trois lignes reviennent.

12. Sans Include on a 0 partout, avec Include on a 3, 3, 3, 2, 2 et 0. La première requête ne lit que Etudiants, la deuxième ajoute un LEFT JOIN sur Notes.

13. Hopper 16,67, Lovelace 15,17, Liskov 13,67, Torvalds 12,25 et Turing 10,33.

### Questions

- Car EF Core ne charge pas les notes si on ne le demande pas, donc la liste reste vide. Charger les notes de chaque étudiant dans la boucle donnerait le problème N+1 : une requête pour les étudiants, puis une par étudiant.
- La somme des coefficients vaut 0, donc on divise par zéro. SQLite renvoie NULL, qui ne rentre pas dans un double : en testant on a une InvalidOperationException (Nullable object must have a value).

## 4. Modifier, supprimer, faire évoluer

14. L'état est Modified avant SaveChanges. Dans le journal on a un UPDATE sur Etudiants qui ne change que la colonne Promo. Si on relance, Alan est déjà en M2 donc l'état reste Unchanged et il n'y a pas d'UPDATE.

15. Il reste 11 notes, les 2 notes de Linus sont supprimées en cascade.

16. 4 lignes sont modifiées, en une seule requête et sans SaveChanges.

17. Oui, la migration ajoute seulement une colonne. Les 5 étudiants sont toujours là et leur Email vaut null.

### Questions

- Car alan est suivi par le contexte. EF Core garde les valeurs du chargement et voit que Promo a changé au moment de SaveChanges.
- Non, ExecuteUpdate modifie directement la base sans passer par le suivi du contexte. En testant, la note déjà chargée garde son coefficient 1, même en refaisant la requête dans le même contexte. Elle passe à 2 avec Reload, ou dans un nouveau contexte.
