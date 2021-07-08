using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Boogle
{
    class Plateau
    {
        SortedList<int, De> debis = new SortedList<int, De>();
        char[,] plateau;

        /// <summary>
        /// Contructeur de la classe Plateau
        /// </summary>
        public Plateau()
        {
            plateau = new char[4, 4];  // on cré une matrice 4*4 
            for (int i = 1; i < plateau.GetLength(0) * plateau.GetLength(1) + 1; i++)
            {
                De ajoutListe = new De(i);
                this.debis.Add(i, ajoutListe);
            }
            int compteur = 0;
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.GetLength(1); j++)
                {
                    compteur++;
                    plateau[i, j] = debis[compteur].FaceSup;
                }
            }
        }

        /// <summary>
        /// Propriété qui représente la matrice dont on se sert pour jouer
        /// </summary>
        public char[,] PlateauMat { get { return plateau; } }

        /// <summary>
        /// Renvoie les informations relatives au Plateau de jeu
        /// </summary>
        /// <returns> Retourne les informations sous chaîne de caractère </returns>
        public override string ToString()
        {
            string description = "";
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    description += " " + debis[i];
                }
                description += "\n"; // on va à la ligne après avoir affiché lettres  
            }
            return description;
        }

        /// <summary>
        /// Vérifie que la matrice n'est pas null ou n'est pas vide afin de l'afficher ensuite
        /// </summary>
        /// <param name="matrice"> Matrice avec laquelle on joue </param>
        /// <returns> Retourne un booléen  qui indique si oui ou non la matrice est vie et/ou null </returns>
        private bool[] NonNullNonVide(char[,] matrice)
        {
            bool nul = false;
            bool vide = false;
            if (matrice == null)
            {
                nul = true;
                Console.WriteLine("La matrice n'a pas d'allocation mémoire");
            }
            else if (matrice.GetLength(0) * matrice.GetLength(1) == 0)
            {
                vide = true;
                Console.WriteLine("La matrice est vide");
            }
            bool[] table = new bool[2];
            table[0] = nul;
            table[1] = vide;
            return table;
        }

        /// <summary>
        /// Permet d'afficher la matrice
        /// </summary>
        public void AfficherMatrice()
        {
            string affichage = "";
            bool[] table = NonNullNonVide(plateau);
            if (!table[0] && !table[1])
            {
                int dim0 = plateau.GetLength(0);
                int dim1 = plateau.GetLength(1);
                for (int i = 0; i < dim0; i++)
                {
                    affichage = "";
                    for (int j = 0; j < dim1; j++)
                    {
                        affichage += plateau[i, j] + " ";
                    }
                    Console.WriteLine(affichage);
                }
            }
        }

        /// <summary>
        /// Méthode qui effectue les tests de vérification relatif au plateau de jeu (unicité du dé sélectionné et adjacence)
        /// </summary>
        /// <param name="plateau"> Matrice de jeu </param>
        /// <param name="mot"> Mot à vérifier </param>
        /// <returns> Retourne un booléen qui indique si le mot est valable au vu du plateau </returns>
        public bool Test_Plateau(char[,] plateau, string mot)
        {
            bool formable = false;
            bool[] unicite = new bool[16]; // Création d'un tableau qui permet d'éviter qu'on utilise deux fois la même lettre
            if (Test_Plateau_Recursif(plateau, mot, unicite))
            {
                formable = true;
            }
            return formable;
        }

        /// <summary>
        /// Méthode qui ajoute à une liste les cellules qui entoure une cellule considérée
        /// </summary>
        /// <param name="plateau"> Matrice de jeu </param>
        /// <param name="positionSomme"> Position des éléments (en sommant ligne et colonne) dont on va chercher les caractères les entourant</param>
        /// <returns> Liste des caractères qui se trouve autour des éléments qui nous intéresse </returns>
        public List<char> Reductionciblee(char[,] plateau, List<int> positionSomme)
        {
            List<char> reductionCiblee = new List<char>();
            for (int n = 0; n < positionSomme.Count; n++)
            {
                int ligne = positionSomme[n] / 4;
                int colonne = positionSomme[n] % 4;

                for (int i = ligne - 1; i < ligne + 2; i++)
                {
                    for (int j = colonne - 1; j < colonne + 2; j++)
                    {
                        if (i != ligne && j != colonne)
                        {
                            reductionCiblee.Add(plateau[(i + plateau.GetLength(0)) % plateau.GetLength(0), (j + plateau.GetLength(1)) % plateau.GetLength(1)]/*, positionSomme[n]*/); // Application du caractère torique de la matrice
                        }
                    }
                }
            }
            return reductionCiblee;
        }

        /// <summary>
        /// Méthode qui ajoute à une liste les cellules qui entoure une cellule considérée dans le cas ou on utilise un int en entrée
        /// </summary>
        /// <param name="plateau"> Matrice de jeu </param>
        /// <param name="positionSomme"> Position d'un élément (en sommant ligne et colonne) dont on va chercher les caractères les entourant</param>
        /// <returns> Liste des caractères qui se trouve autour des éléments qui nous intéresse </returns>
        public List<char> Reductionciblee2(char[,] plateau, int positionSomme)
        {
            List<char> reductionCiblee = new List<char>();
            int ligne = positionSomme / 4;
            int colonne = positionSomme % 4;

            for (int i = ligne - 1; i < ligne + 2; i++)
            {
                for (int j = colonne - 1; j < colonne + 2; j++)
                {
                    if (i != ligne && j != colonne)
                    {
                        reductionCiblee.Add(plateau[(i + plateau.GetLength(0)) % plateau.GetLength(0), (j + plateau.GetLength(1)) % plateau.GetLength(1)]/*, positionSomme[n]*/); // Application du caractère torique de la matrice
                    }
                }
            }
            return reductionCiblee;
        }

        /// <summary>
        /// Vérifie que le caractère suivant appartient à l'environnement du caractère précédent
        /// </summary>
        /// <param name="reductionCiblee"> Liste des caractère dans laquelle on doit chercher </param>
        /// <param name="mot"> Caractère à rechercher </param>
        /// <returns> Retourne un entier qui indique combien de fois le caractère est présent </returns>
        public int Adjacence(List<char> reductionCiblee, char mot)
        {
            int temp = 0;
            for (int i = 0; i < reductionCiblee.Count; i++)
            {
                if (reductionCiblee[i] == mot)
                {
                    temp++;
                }
            }
            return temp;
        }

        /// <summary>
        /// Retourne tous les emplacements des caractères recherchés
        /// </summary>
        /// <param name="plateau"> Matrice de jeu</param>
        /// <param name="mot"> Mot dont on va chercher un caractère </param>
        /// <param name="compteur"> Indique quel caractère du mot on chercher </param>
        /// <returns> Retourne les positions en sommant ligne et colonne du caractère recherché</returns>
        public List<int> RecherchePosition(char[,] plateau, string mot, int compteur)
        {
            List<int> positionSomme = new List<int>();
            int compte = 1;
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.GetLength(1); j++)
                {
                    if (plateau[i, j] == mot[compteur])
                    {
                        positionSomme.Add(compte);
                    }
                    compte++;
                }
            }
            return positionSomme;
        }

        /// <summary>
        /// Méthode qui doit tester si un mot appartient ou non
        /// </summary>
        /// <param name="plateau"> Matrice jeu</param>
        /// <param name="mot"> Mot dont on cherche à vérifier l'adjacence</param>
        /// <param name="unicite"> Tableau de bool vérifiant si oui ou non un dé est déjà utilisé </param>
        /// <param name="compteur"> Compteur</param>
        /// <param name="positionSommePrec"> Permet de réduire les possibilités dans le cas ou une lettre apparaît plusieurs fois</param>
        /// <returns> Retourne un booléen qui indique si le mot peut être formé </returns>
        public bool Test_Plateau_Recursif(char[,] plateau, string mot, bool[] unicite, int compteur = 0)
        {
            if (compteur == mot.Length - 2)
            {
                return true;
            }
            else
            {
                List<int> positionSomme = RecherchePosition(plateau, mot, compteur); // Renvoie l'index des caractères pouvant correspondre au n-ième élément du mot
                List<char> reductionCiblee = Reductionciblee(plateau, positionSomme); // Renvoie la liste des caractères qui entoure ce caractère cible

                if (positionSomme.Count == 1 && unicite[positionSomme[0]]) // Cas ou on a une seule fois la lettre initiale
                {
                    unicite[positionSomme[0]] = false;
                    if (Adjacence(reductionCiblee, mot[compteur + 1]) == 1) // Cas ou on a une seule fois la lettre suivante
                    {
                        return Test_Plateau_Recursif2(plateau, mot, unicite, compteur++, positionSomme[0]);
                    }
                    else if (Adjacence(reductionCiblee, mot[compteur + 1]) > 1) // Cas ou on l'a plus d'une fois
                    {
                        return false;
                    }
                    else // Cas ou on ne l'a pas
                    {
                        return false;
                    }
                }
                else if (positionSomme.Count > 1) // Cas ou on a plusieurs fois la lettre initiale
                {
                    bool unique = true;
                    for (int i = 0; i < positionSomme.Count; i++)
                    {
                        if (!unicite[positionSomme[i]])
                        {
                            unique = false;
                        }
                    }
                    if (unique)
                    {
                        for (int i = 0; i < positionSomme.Count; i++)
                        {
                            if (!Test_Plateau_Recursif2(plateau, mot, unicite, compteur++, positionSomme[i]))
                            {
                                positionSomme.RemoveAt(i);
                            }
                        }
                        if (Adjacence(reductionCiblee, mot[compteur + 1]) == 1) // Cas ou on a une seule fois la lettre suivante
                        {
                            return Test_Plateau_Recursif2(plateau, mot, unicite, compteur++, positionSomme[0]);
                        }
                        else if (Adjacence(reductionCiblee, mot[compteur + 1]) > 1) // Cas ou on l'a plus d'une fois
                        {
                            return false;
                        }
                        else // Cas ou on ne l'a pas
                        {
                            return false;
                        }
                        //return Test_Plateau_Recursif2(plateau, mot, unicite, compteur++, positionSomme[0]);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// Retourne l'emplacement du caractère
        /// </summary>
        /// <param name="plateau"> Matrice de jeu </param>
        /// <param name="positionSommePrec"> Ancien emplacement qui doit permettre de déduire le nouveau </param>
        /// <param name="mot"> Caractère que l'on doit trouver pour le nouvel emplacement </param>
        /// <returns></returns>
        public int RecherchePosition2(char[,] plateau, int positionSommePrec, char mot)
        {
            int positionSomme = -1;

            int ligne = positionSommePrec / 4;
            int colonne = positionSommePrec % 4;
            int compteur = 1;
            for (int i = ligne - 1; i < ligne + 2; i++)
            {
                for (int j = colonne - 1; j < colonne + 2; j++)
                {
                    if (i != ligne && j != colonne)
                    {
                        if (plateau[(i + plateau.GetLength(0)) % plateau.GetLength(0), (j + plateau.GetLength(1)) % plateau.GetLength(1)] == mot)
                        {
                            positionSomme = compteur;
                        }
                    }
                    compteur++;
                }
            }
            return positionSomme;

        }

        /// <summary>
        /// Méthode qui doit tester si un mot appartient ou non dans le cas ou on connaît le point de départ
        /// </summary>
        /// <param name="plateau"> Matrice jeu</param>
        /// <param name="mot"> Mot dont on cherche à vérifier l'acceptabilité </param>
        /// <param name="unicite"> Tableau de bool vérifiant si oui ou non un dé est déjà utilisé </param>
        /// <param name="compteur"> Compteur </param>
        /// <param name="positionSommePrec"> Permet de savoir ou était le début de la chaîne testée </param>
        /// <returns></returns>
        public bool Test_Plateau_Recursif2(char[,] plateau, string mot, bool[] unicite, int compteur, int positionSommePrec)
        {
            if (compteur == mot.Length - 2)
            {
                return true;
            }
            else
            {
                int positionSomme = RecherchePosition2(plateau, positionSommePrec, mot[compteur]);
                if (unicite[positionSomme])
                {
                    unicite[positionSomme] = false;
                    List<char> reductionCiblee = Reductionciblee2(plateau, positionSommePrec); // Renvoie la liste des caractères qui entoure ce caractère cible
                    if (Adjacence(reductionCiblee, mot[compteur + 1]) == 1) // Cas ou on a une seule fois la lettre suivante
                    {

                        return Test_Plateau_Recursif2(plateau, mot, unicite, compteur++, positionSomme);
                    }
                    else if (Adjacence(reductionCiblee, mot[compteur + 1]) > 1) // Cas ou on l'a plus d'une fois
                    {
                        return false;
                    }
                    else // Cas ou on ne l'a pas
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
