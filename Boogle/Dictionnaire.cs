using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Boogle
{
    public class Dictionnaire
    {
        string langue;
        List<string> motDeux = new List<string>(); // Création de toutes les listes 
        List<string> motTroi = new List<string>();
        List<string> motQuat = new List<string>();
        List<string> motCinq = new List<string>();
        List<string> motSix = new List<string>();
        List<string> motSept = new List<string>();
        List<string> motHuit = new List<string>();
        List<string> motNeuf = new List<string>();
        List<string> motDix = new List<string>();
        List<string> motOnze = new List<string>();
        List<string> motDouz = new List<string>();
        List<string> motTrei = new List<string>();
        List<string> motQuaz = new List<string>();
        List<string> motQuin = new List<string>();
        Dictionary<int, List<string>> superDico; // Création du Dictionnaire principal qui regroupera les listes

        /// <summary>
        /// Constructeur du dictionnaire :
        /// Pour éviter de traiter de multiples conditions on fait le choix de créer un dictionnaire regroupant les listes qui contiennent les mots en fonction de leur longueur.
        /// Cela permet d'associer la liste, à la valeur correspondante à la longueur des mots qu'elle contient.
        /// 
        /// Pour remplir ces listes, on lit un fichier texte de 28 lignes maximum (2x14 lignes pour annoncer la longueur des mots et 2x14 lignes pour les mots (on considère ceux de longueur comprise entre 2 et 15))
        /// 
        /// </summary>
        /// <param name="langue"> Langue du dictionnaire</param>
        public Dictionnaire(string langue)
        {
            this.langue = langue;
            Dictionary<int, List<string>> superDico = new Dictionary<int, List<string>>();
            superDico.Add(2, motDeux);
            superDico.Add(3, motTroi);
            superDico.Add(4, motQuat);
            superDico.Add(5, motCinq);
            superDico.Add(6, motSix);
            superDico.Add(7, motSept);
            superDico.Add(8, motHuit);
            superDico.Add(9, motNeuf);
            superDico.Add(10, motDix);
            superDico.Add(11, motOnze);
            superDico.Add(12, motDouz);
            superDico.Add(13, motTrei);
            superDico.Add(14, motQuaz);
            superDico.Add(15, motQuin);

            StreamReader lecture = new StreamReader("MotsPossibles_" + langue.ToLower() + ".txt"); // On prend en considération la langue -> les noms de fichiers devront être standardisés
            string ligne = "" ;
            int nombreLettres;
            for (int i = 1; i < 29; i += 2)  // On prend en considération les mots de 2 à 15 lettres plus la ligne qui les annonce soit 28 lignes...
            {
                ligne = lecture.ReadLine(); // Lecture de la longueur des mots qui vont suivre
                nombreLettres = Convert.ToInt32(ligne.ToString()); // Sauvegarde de cette longueur
                ligne = lecture.ReadLine(); // Lecture des mots 
                string ajoutDico;
                for (int j = 0; j < ligne.Length; j += nombreLettres + 1) // On ajoute les mots via leur longueur et en sautant l'espace
                {
                    ajoutDico = ligne.Substring(j,nombreLettres); // On ajoute la sous-chaîne de caractère qui commence à l'indice j et fait une longueur égale au nombre de lettre dans le mot
                    superDico[nombreLettres].Add(ajoutDico); // Ajout à la liste qui les sauvegardera le tant de l'exécution du programme
                }
            }
            lecture.Close();
            this.superDico = superDico;
        }

        /// <summary>
        /// Propriété qui donne l'accès au dictionnaire
        /// </summary>
        public Dictionary<int, List<string>> SuperDico { get { return superDico; }}

        /// <summary>
        /// Méthode qui renvoie sous forme de string, les informations relatives au dictionnaire
        /// </summary>
        /// <returns> Retourne une chaîne de caractère qui donne les informations sur le Dictionnaire </returns>
        public string toString()
        {
            string retour = "Langue : " + langue;
            for (int i = 2; i < superDico.Count + 2; i++)
            {
                retour += "\n" + "Nombre de mots de " + i + " lettres : " + superDico[i].Count; 
            }
            return retour;
        }

        /// <summary>
        /// Méthode qui va tester si le mot médian dans la recherche dichotomique est placé après le mot recherché
        /// </summary>
        /// <param name="test"> Mot médian de la liste des mots de longueur définie dans laquelle on fait la recherche</param>
        /// <param name="tentative"> Mot proposé par le joueur</param>
        /// <returns> Retourne un booléen qui indique si le mot testé est placé après le mot qui est au milieu de la liste </returns>
        public bool ComparateurClassementMot(string test, string tentative)
        {
            bool retour = true;
            int longueurmin = Math.Min(test.Length, tentative.Length);
            for (int i = 0; i < longueurmin; i++)
            {
                if (Convert.ToInt32(test[i]) > Convert.ToInt32(tentative[i]))
                {
                    retour = true;
                    i = longueurmin;
                }
                else if (Convert.ToInt32(test[i]) < Convert.ToInt32(tentative[i]))
                {
                    retour = false;
                    i = longueurmin;
                }
            }
            return retour; // Ligne qui ne sert qu'à permettre d'éviter l'erreur de compilation
        }

        /// <summary>
        /// Méthode recursive qui fait une recherche dichotomique dans la liste, des mots de longueur égale à la longueur du mot à vérifier, qui appartient au super dictionnaire
        /// </summary>
        /// <param name="superDico"> Super dictionnaire regroupant les listes qui contiennent les mots valides, classée par la longueur des mots qu'elles contiennent</param>
        /// <param name="tentative"> Mot que l'on doit vérifier</param>
        /// <param name="debut"> Indice du premier élement de la liste dans laquelle on recherche</param>
        /// <param name="fin"> Indice du dernier élement de la liste dans laquelle on recherche</param>
        /// <returns> Retourne un booléen qui indique si le mot tester appartient ou non à la liste des mots existant </returns>
        public bool RechercheDichoRecursif(Dictionary<int, List<string>> superDico, string tentative, int debut, int fin, int com = 0)
        {
            com++; 
            if (superDico[tentative.Length][(debut + fin) / 2] == tentative)
            {
                return true;
            }
            else
            {
                if (debut == fin)
                {
                    return false;
                }
                else if (ComparateurClassementMot(superDico[tentative.Length][(debut + fin) / 2], tentative))
                {
                    return RechercheDichoRecursif(superDico, tentative, debut, Convert.ToInt32(Math.Truncate(Convert.ToDecimal((debut + fin) / 2))), com);
                }
                else
                {
                    return RechercheDichoRecursif(superDico, tentative, Convert.ToInt32(Math.Truncate(Convert.ToDecimal((debut + fin) / 2))) + 1, fin, com);
                }
            }            
        }
    }

}
