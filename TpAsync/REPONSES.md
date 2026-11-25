# TP 3 : Exceptions, JSON, async et HTTP

## 1. Exceptions

- Car ce sont les deux erreurs qu'on attend : une ligne mal écrite ou une note hors limites. Avec Exception on attraperait aussi les vrais bugs et on ne les verrait plus.
- Je préfère TryParse quand une mauvaise valeur peut arriver normalement, comme dans un fichier. Dans ParserStricte je l'ai utilisé car ça me permet de mettre mon propre message avec le numéro de ligne, sans lever une première exception pour rien.
- `throw;` garde la pile d'appels d'origine alors que `throw e;` la recommence à partir du catch. En testant, avec `throw e;` la méthode qui avait planté au départ n'apparaît plus dans la pile.

## 2. Sauvegarder la promo en JSON

8. Les noms sont en camelCase (nom, prenom, valeur...) à cause des conventions du Web. Il faut mettre les mêmes options dans Charger, j'avais oublié et le coef n'était pas reconnu.

10. On a une JsonException. Je l'attrape dans Charger et je renvoie une liste vide, donc on repart de notes.csv.

11. On a une ArgumentOutOfRangeException car le JSON passe par le constructeur du record, donc la vérification se fait aussi. C'est bien que la note soit refusée, par contre je n'attrape pas cette exception donc le programme s'arrête.

### Questions

- On obtient null si le fichier contient juste `null`. Dans ce cas je renvoie une liste vide avec `??`.
- Le séparateur n'est pas le même sous Windows (\\) et sous Linux (/). Docker tourne sous Linux donc un chemin écrit à la main pour Windows ne marcherait pas, Path.Combine met le bon.

## 3. Passer en asynchrone

13. On a le warning CS4014. À l'exécution le programme continue sans attendre la sauvegarde, donc il peut se terminer avant que le fichier soit écrit et on ne verrait pas les erreurs.

14. Environ 3 secondes (3004 ms).

15. Environ 1 seconde (1001 ms) car les trois attentes se font en même temps.

16. Oui ça marche, en 3 secondes aussi. C'est déconseillé car .Result bloque le thread pendant l'attente, et dans une appli avec une interface ça peut même tout bloquer.

## 4. Interroger l'API

17. J'ai mis int car c'est un nombre entier d'habitants. Si l'API ne donnait pas la population on aurait 0, avec int? on aurait null.

19. Pour 99999 l'API renvoie un tableau vide.

22. Dernier essai : 223 ms en parallèle et 1206 ms l'une après l'autre. Ça change pas mal d'un essai à l'autre, j'ai même eu 209 ms contre 141 ms une fois. Je pense que c'est parce que la version parallèle passe en premier et doit ouvrir les connexions.

23. Une HttpRequestException. Je l'attrape dans CommunesAsync avec un message.

24. Une TaskCanceledException, testé avec 1 ms.

### Questions

- Chaque HttpClient ouvre ses propres connexions, donc en créer un par requête est plus lent et on peut finir par manquer de ports.
- Les étudiants sont indépendants, si une requête échoue on veut quand même afficher les autres. Pour ça j'ai mis le try/catch dans CommunesAsync, elle renvoie une liste vide et on affiche "commune inconnue".
