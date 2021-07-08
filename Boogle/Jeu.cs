using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Boogle
{
    class Jeu
    {
        /// <summary>
        /// Méthode qui contient l'affichage du menu et retourne la paramètre choisi
        /// </summary>
        /// <returns> Retourne le paramètre choisi par l'utilisateur </returns>
        static int Menu() 
        {
            int choixdujeu = 0;
            while (choixdujeu != 1 && choixdujeu != 2 && choixdujeu != 3 && choixdujeu != 4 && choixdujeu != 5 && choixdujeu != 6)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                                   Menu Boogle                                                        ");
                Console.WriteLine("                                                                                                                      ");
                Console.WriteLine("||       1. Jouer au Boogle avec 2 joueurs                                                                          ||");
                //Console.WriteLine("||       2. Jouer au Boogle contre l'IA                                                                             ||");
                Console.WriteLine("||       2. Paramètres                                                                                              ||");
                Console.WriteLine("||       3. Règles et informations                                                                                  ||");
                Console.WriteLine("||       4. Quitter le jeu                                                                                          ||");
                Console.WriteLine("                                                                                                                      ");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                                                                                                      ");
                Console.WriteLine("                                                                                                                      ");
                Console.WriteLine("-> ?   ");
                choixdujeu = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }

            return choixdujeu;
        }

        /// <summary>
        /// Méthode qui contien l'affichage du menu des paramètres
        /// </summary>
        /// <param name="afficherMenu"> Regarde si le menu doit être affiché ou non</param>
        /// <returns> Retourne les paramètres sous forme de tableau de string </returns>
        static string[] Parametres(bool afficherMenu)
        {
            string[] parametrage = new string[2];
            string langue = "francais";
            string tempsSeconde = "60";
            int choix = 0;
            if (afficherMenu)
            {
                while (choix != 1 && choix != 2 && choix != 3)
                {
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                               Menu des Paramètres                                                    ");
                    Console.WriteLine("                                                                                                                      ");
                    Console.WriteLine("||       1. Changer la langue du dictionnaire                                                                       ||");
                    Console.WriteLine("||       2. Changer le temps par joueur                                                                             ||");
                    Console.WriteLine("||       3. Revenir au menu précédent                                                                               ||");
                    Console.WriteLine("                                                                                                                      ");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                                                                                                      ");
                    Console.WriteLine("                                                                                                                      ");
                    Console.WriteLine("-> ?   ");
                    choix = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                }
                if (choix == 1)
                {
                    Console.WriteLine("Choisissez la langue (francais par défaut) : ");
                    Console.WriteLine("-> ?   ");
                    langue = Console.ReadLine().ToUpper().ToLower();
                    Console.Clear();
                }
                if (choix == 2)
                {
                    Console.WriteLine("Choisissez la durée par manche (en secondes) : ");
                    Console.WriteLine("-> ?   ");
                    tempsSeconde = Console.ReadLine();
                    Console.Clear();
                }
            }
            parametrage[0] = langue;
            parametrage[1] = tempsSeconde;
            return parametrage;
        }

        /// <summary>
        /// Méthode qui calcule le score en fonction de la longueur du mot
        /// </summary>
        /// <param name="mot"> Mot dont la longueur définie le score </param>
        /// <returns> Retourne un entier correspondant au score à ajouter au joueur </returns>
        static int Score(string mot)
        {
            int score = 0;
            switch (mot.Length)
            {
                case 3:
                    score = 2;
                    break;

                case 4:
                    score = 3;
                    break;

                case 5:
                    score = 4;
                    break;

                case 6:
                    score = 5;
                    break;

                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    score = 11;
                    break;

                default:
                    score = 0;
                    break;
            }
            return score;
        }

        /// <summary>
        /// Méthode qui exécute une partie de Boogle pour un joueur
        /// </summary>
        /// <param name="Dico"> Dictionnaire à utiliser durant la partie </param>
        /// <param name="Joueur"> Joueur qui joue </param>
        /// <param name="temps"> Temps à laisser au joueur </param>
        static void Boogle(Dictionnaire Dico, Joueur Joueur, string temps)
        {
            Console.Clear();
            Plateau Plateau1 = new Plateau();
            DateTime tempsRestant = DateTime.Now;
            DateTime finPartie = DateTime.Now.AddSeconds(Convert.ToInt32(temps));
            Console.WriteLine("Tenez-vous prêt " + Joueur.Nom + '\n' + "Appuyez sur une touche pour débuter, vous aurez alors 60 secondes...");
            Console.ReadKey();
            while (finPartie > tempsRestant)
            {
                Plateau1.AfficherMatrice();
                Console.WriteLine("Plus que " + (finPartie - tempsRestant).TotalSeconds + " secondes");
                Console.WriteLine("Veuillez entrer un mot : ");
                Console.WriteLine("-> ?   ");
                string mot = Console.ReadLine();
                if (mot.Length >= 3 && Joueur.Contain(mot) /*&& Plateau1.Test_Plateau(Plateau1.PlateauMat, mot)*/ && Dico.RechercheDichoRecursif(Dico.SuperDico, mot.ToUpper(), 0, Dico.SuperDico[mot.Length].Count))
                {
                    Joueur.Add_Mot(mot);
                    Joueur.Score += Score(mot);
                    Console.Clear();
                    Console.WriteLine("Mot accepté ! Félicitation : " + mot + " a été ajouté vous rapportant " + Score(mot) + " points");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Mot non valide");
                }
                tempsRestant = DateTime.Now;                
            }
            Console.WriteLine("Temps écoulé !");
            System.Threading.Thread.Sleep(5000);
        }

        /// <summary>
        /// Main...
        /// </summary>
        static void Main()
        {

            bool quitter = false;

            while (!quitter)
            {
                Console.Clear();
                int choixmode = Menu();
                bool parametresdef = false;
                string[] parametrage = new string[2];
                switch (choixmode)
                {
                    //case 1:
                    case 1:
                        if (!parametresdef)
                        {
                            parametrage = Parametres(false);
                        }
                        Dictionnaire Dico = new Dictionnaire(parametrage[0]);
                        Console.WriteLine("Veuillez entrer le nom du Joueur n°1 : ");
                        Console.WriteLine("-> ?   ");
                        Joueur Joueur1 = new Joueur(Console.ReadLine(), 0);
                        if (choixmode == 1)
                        {
                            Console.WriteLine("Veuillez entrer le nom du Joueur n°2 : ");
                            Console.WriteLine("-> ?   ");
                            Joueur Joueur2 = new Joueur(Console.ReadLine(), 0);
                            Boogle(Dico, Joueur1, parametrage[1]);
                            Boogle(Dico, Joueur2, parametrage[1]);
                            if (Joueur1.Score > Joueur2.Score)
                            {
                                Console.WriteLine("Victoire de " + Joueur1.ToString() + " sur " + Joueur2.ToString() + '\n' + "Bravo à vous et bonne continuation !");
                            }
                            else if (Joueur1.Score == Joueur2.Score)
                            {
                                Console.WriteLine("Egalité entre " + Joueur1.ToString() + " et " + Joueur2.ToString() + '\n' + "Bravo à vous et bonne continuation !");
                            }
                            else
                            {
                                Console.WriteLine("Victoire de " + Joueur2.ToString() + " sur " + Joueur1.ToString() + '\n' + "Bravo à vous et bonne continuation !");
                            }
                            Console.WriteLine("Appuyez sur une touche pour continuer...");
                            Console.ReadKey();
                        }
                        else
                        {
                            //IA = new IA();
                        }

                        break;
                    case 2:
                        parametrage = Parametres(true);
                        parametresdef = true;
                        break;

                    case 3:
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("                                                                                                                          ");
                        Console.WriteLine("                                         Bonjour et Bienvenue sur le Boogle                                               ");
                        Console.WriteLine("                                      Conçu et développé par Mathilde & Clément                                           ");
                        Console.WriteLine("                                                                                                                          ");
                        Console.WriteLine("                                                                                                                          ");
                        Console.WriteLine(" Le Boogle est un jeu consistant à reconstituer des mots de la plus grande longueur possible,                             ");
                        Console.WriteLine(" à partir d'une grille alétoire, générée en tirant au hasard les faces de 16 dés.                                         ");
                        Console.WriteLine(" Les deux joueurs jouent l'un après l'autre, ils disposent d'un temps défini (1 min par défault)                          ");
                        Console.WriteLine(" Pour retrouver le maximum de mot. Chaque mot rapporte un certain nombre de points en fonction de sa longueur             ");
                        Console.WriteLine(" Tableau des scores :                                                                                                     ");
                        Console.WriteLine("  Taille d'un mot | 3 | 4 | 5 | 6 | 7 et plus |                                                                           ");
                        Console.WriteLine(" Nombre de points | 2 | 3 | 4 | 5 |    11     |                                                                           ");
                        Console.WriteLine(" Bon jeu !                                                                                                                ");
                        Console.WriteLine("                                                                                                                          ");
                        Console.WriteLine(" Appuyer sur une touche pour continuer...                                                                                 ");
                        Console.ReadKey();
                        break;

                    case 4:
                        quitter = true;
                        break;
                }
                //Console.WriteLine(Convert.ToInt32(DateTime.Now));
            }

            //Dictionnaire Essai = new Dictionnaire("francais");
            //Console.WriteLine(Essai.toString());
            ////Console.WriteLine(Essai.ComparateurClassementMot2("acte", "zoulou"));
            //Console.WriteLine(Essai.RechercheDichoRecursif(Essai.SuperDico, "lionne".ToUpper(), 0, Essai.SuperDico["lionne".Length].Count - 1));
        }
    }
}
