using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle
{
    public class Joueur
    {
        string nom;
        int score;
        List<string> motstrouves;

        /// <summary>
        /// Constructeur de la classe Personne
        /// </summary>
        /// <param name="nom"> Représente le nom du joueur </param>
        /// <param name="score"> Représente le score du joueur </param>
        public Joueur(string nom, int score)
        {
            this.nom = nom;
            this.score = score;
            this.motstrouves = new List<string>();
        }

        /// <summary>
        /// Propriété qui représente le score du joueur
        /// </summary>
        public int Score { get { return score; } set { score = value; } }

        /// <summary>
        /// Propriété qui représente le nom du joueur
        /// </summary>
        public string Nom { get { return nom; } }

        /// <summary>
        /// Test si le mot essayé par le joueur a déjà été trouvé
        /// </summary>
        /// <param name="mot"> Mot à tester </param>
        /// <returns> Booléen qui indique si oui ou non le mot a déjà été trouvé </returns>
        public bool Contain(string mot)
        {
            if (motstrouves.Contains(mot))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie
        /// </summary>
        /// <param name="mot"> Mot à ajouter </param>
        public void Add_Mot(string mot)
        {
            motstrouves.Add(mot);
        }

        /// <summary>
        /// Méthode qui renvoie les informations relatives à un joueur
        /// </summary>
        /// <returns> Returne une chaîne de caractère contenant l'ensemble des informations sur le joueur </returns>
        public override string ToString()
        {
            string ensemblemot = "";
            foreach (string word in motstrouves)
            {
                ensemblemot += word + " ";
            }
            return (nom + " avec un score de " + score + " les mots qu'il a trouvé sont : " + ensemblemot);
        }
    }
}