# TP 2 : Délégués, lambdas et LINQ

## 0. Mise en place

### 0.3

1. On a 13 notes valides et 6 inscrits. La ligne de Barbara en Algo (abc) est écartée.

2. On obtient un IEnumerable<Note>. Avec Where le type reste Note? donc on a un warning dès qu'on fait n.Valeur, alors qu'avec OfType il n'y a plus rien à vérifier après.

4. C'est la ligne 14 qui est signalée sur Console.Error.

## 1. Délégués et lambdas

### 1.1

3. C'est un Func<Note, bool>. `var c = n => n.Valeur >= 15;` ne compile pas (CS8917) car le compilateur ne connaît pas le type de n, donc il ne peut pas deviner celui de la lambda.

### 1.3

- On obtient 10 puis 6. La lambda garde la variable seuil et pas sa valeur, donc au 2e appel elle voit 15.
- En Java ça ne compile pas car une variable utilisée dans une lambda ne doit pas être modifiée (effectively final).
- Il faut copier la valeur dans une autre variable qu'on ne modifie plus (seuilFixe). Sinon AuDessusDe(12) marche aussi pour figer le seuil, mais avec > au lieu de >=.

## 2. Premières requêtes

### 2.1

4. Avec deux OrderBy le 2e refait tout le tri, donc on perd le classement par matière. Avec ThenBy la matière reste le premier critère.

5. Top 3 : Grace 18, Grace 17, Ada 16. Ensuite Linus 16, Ada 15, Barbara 15.

### 2.2

1. 13 notes, 1 seule sous 10 (Alan en Algo) et une moyenne simple de 13,77.

2. Max donne juste la valeur (18) et MaxBy donne la note complète (Grace, Algo, 18).

3. La moyenne pondérée est de 13,72.

4. Les deux donnent "Ada, Alan, Grace, Linus, Barbara". Je préfère string.Join, Aggregate est un peu compliqué pour juste coller des prénoms.

### 2.3

1. Ça renvoie null car Linus n'a pas de note en BD. Avec First on a une InvalidOperationException.

2. Pour Ada en Algo ça marche. Sur toutes ses notes on a une InvalidOperationException car il y en a 3 et Single en veut une seule.

3. Oui pour la note sous 10, et oui pour la BD : tous ceux qui ont une note en BD ont au moins 12 (Linus n'en a pas). All sur une séquence vide renvoie true.

## 3. Regrouper et joindre

### 3.1

1. C'est un IGrouping<string, Note>, donc une clé (la matière) avec ses notes.

2. BD : 4 notes, moyenne 14,50. Réseau : 5 notes, 13,60. Algo : 4 notes, 13,25. Les majors sont Grace en Algo et en BD, et Linus en Réseau.

3. Grace 16,67, Ada 15,17, Barbara 13,67, Linus 12,25, Alan 10,33.

4. On a le même résultat mais CountBy est plus court.

### 3.2

2. M1 : 13,00 et M2 : 15,00.

3. Margaret n'a aucune note et Join ne garde que ceux qui ont une correspondance, comme un INNER JOIN en SQL. Je l'ai retrouvée avec Where et Any, puis avec GroupJoin.

4. Pour un prénom absent on a une séquence vide, pas d'exception.

### 3.3

2. SelectMany donne toutes les valeurs à plat (List<double>). Avec Select on aurait une liste de listes.

## 4. L'évaluation différée

1. On obtient 6, puis 7, puis 8 / 7. bonnes est une requête et elle est relancée à chaque Count(), donc elle voit les notes ajoutées entre temps. figees a été calculée avec ToList() avant le dernier ajout donc elle reste à 7.

2. Avec deux Count() chaque note est testée 2 fois (26 tests). Avec ToList() d'abord c'est une seule fois (13 tests).

3. Any() s'arrête à la première note qui correspond. Ici c'est la toute première, donc 1 seul test au lieu de 13.

4. Le résultat est le même. Avec le Where avant on trie 6 notes, avec le Where après on en trie 13.

## 5. Réécrire la classe Promo

1. On ferait 0 / 0 donc on obtient NaN (pas d'exception car ce sont des double). Pour l'éviter je renvoie 0 s'il n'y a pas de notes.

2. 1. Grace 16,67 Très bien, 2. Ada 15,17 Bien, 3. Barbara 13,67 Assez bien, 4. Linus 12,25 Assez bien, 5. Alan 10,33 Passable.

3. C'est plus court qu'à la séance 1, presque toutes les méthodes tiennent en une ou quelques lignes. C'est aussi plus facile à modifier, pour changer le tri on change juste la lambda. Par contre il faut connaître les méthodes de LINQ pour bien lire.

4. Oui elle est recalculée à chaque appel. Avec 13 notes ce n'est pas un problème. Il faudrait la mettre en cache avec beaucoup de données ou si on l'appelle souvent.
