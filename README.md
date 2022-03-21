# Projet-A2S3
## Boogle

Le jeu commence par le mélange d’un plateau (carré) de 16 dés à 6 faces. Chaque dé possède une
lettre différente sur chacune de ses faces. Les dés sont lancés sur le plateau 4 par 4, et seule leur face
supérieure est visible. Le lancement de dé est traduit par un tirage aléatoire d’une face parmi les
6 d’un dé et ce pour tous les dés. Après cette opération, un compte à rebours de N minutes est lancé
qui établit la durée de la partie.
Chaque joueur joue l’un après l’autre pendant un laps de temps de 1 mn.
Chaque joueur cherche des mots pouvant être formés à partir de lettres adjacentes du plateau. Par
adjacente, il est sous-entendu horizontalement, verticalement ou en diagonale. Les mots doivent
être de 3 lettres au minimum, peuvent être au singulier ou au pluriel, conjugués ou non, mais ne
doivent pas utiliser plusieurs fois le même dé pour le même mot. Les joueurs saisissent tous les mots
qu’ils ont trouvés au clavier. Un score par joueur est mis à jour à chaque mot trouvé et validé.

Le calcul de points se fait de la manière suivante : Un mot n’est accepté qu’une fois au cours du jeu
par joueur.
En fonction de la taille du mot des points sont octroyés
