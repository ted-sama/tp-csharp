# TP 1 : Introduction à C#

## 1. Premier projet

2. On vise .NET 10 (net10.0). Nullable et ImplicitUsings sont activés tous les deux.

4. Le .dll c'est le programme compilé en IL et pas en code machine. En Java ça serait le .jar, donc `dotnet TpCsharp.dll` revient à faire `java -jar`.

5. Il n'y a pas de .csproj, dotnet génère le projet tout seul derrière. C'est pratique pour tester un truc rapidement mais dès qu'on a plusieurs fichiers il faut un vrai projet.

## 2. Types, chaînes et null

### 2.1

Ça affiche c1.X = 9, s1.X = 1, True, False et True.

- PointC est une classe donc `c2 = c1` copie juste la référence, on modifie le même objet. PointS est une struct donc s2 est une vraie copie et s1.X reste à 1.
- En Java on aurait false car == compare les références. En C# le == des string compare le texte.
- Le m veut dire decimal. Avec des double 0.1 + 0.2 donne 0.30000000000000004, avec des decimal ça tombe juste. C'est important dès qu'on manipule de l'argent.

### 2.2

1. On a deux warnings : CS8600 car on met un null possible dans un string, et CS8602 car on fait .Length dessus.

2. Si on ferme l'entrée (Ctrl+D ou Ctrl+Z) il n'y a plus rien à lire donc ReadLine() renvoie null.

5. Le warning devient une erreur et le projet ne compile plus.

## 3. Contrôle et collections

4. `dico["Inconnu"]` plante avec une KeyNotFoundException. Avec TryGetValue on a juste false si la clé n'existe pas.

## 4. Classe Etudiant

### 4.1

- Il faut override. Si on l'oublie on a le warning CS0114 (donc une erreur chez nous) et Console.WriteLine(ada) afficherait "Ecole.Etudiant".
- `ada._notes` ne compile pas, erreur CS0122 car le champ est privé.
- Avec une List publique on pourrait faire `ada.Notes.Add(45)` sans passer par AjouterNote, donc sans la vérification. J'ai mis `public IReadOnlyList<double> Notes => _notes.AsReadOnly();`

### 4.2

3. Oui ça compile toujours. Par défaut une classe est internal, donc visible seulement dans le projet. Avec un projet de tests à part on ne verrait plus Promo.

## 5. Héritage et interfaces

### 5.1

4. Erreur CS0509, on ne peut pas hériter d'une classe sealed. Pour Carre je passe aussi le nom "Carré" à Rectangle, sinon il s'affichait "Rectangle".

### 5.2

Oui, Sort() utilise CompareTo donc les formes sont triées par aire croissante. Le rayon s'affiche bien : Dessiner() est dans Forme mais c'est le Decrire() de Cercle qui est appelé.

L'aire totale est de 41,78 et le plus grand périmètre est celui du cercle de rayon 2,5 (15,71).

### 5.3

1. Erreur CS0506, on ne peut pas override une méthode qui n'est pas virtual.

3. Non, `c.Decrire()` affiche le rayon mais pas `f.Decrire()` alors que c'est le même objet. Avec override c'est le vrai type de l'objet qui compte (Cercle). Avec new c'est le type de la variable (Forme), la méthode de Forme est juste cachée. En Java les deux lignes afficheraient le rayon car les méthodes sont virtuelles par défaut.

## 6. Records et exceptions

1. Deux Note avec les mêmes valeurs donnent true avec == car un record compare le contenu. Deux Etudiant avec les mêmes données donnent false : c'est une classe donc == compare les références.

4. Les lignes 5 (note abc) et 7 (note 25) sont signalées puis ignorées.

6. Pour la moyenne pondérée je mettrais le calcul dans Etudiant, à la place de Moyenne(). Il faudrait garder le coef de chaque note, donc avoir une List<Note> au lieu d'une List<double>, et AjouterNote devrait aussi prendre le coef.
