using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Boogle
{
    class De
    {
        int numero;
        char[] de;
        char lettre;
        Random r = new Random();

        /// <summary>
        /// Constructeur de la classe De
        /// </summary>
        /// <param name="numero"> Numéro du dé considéré </param>
        public De(int numero)
        {
            this.numero = numero;
            string ligne = "";
            StreamReader lecture = new StreamReader("Des.txt");
            for (int i = 0; i < numero; i++)
            {
                ligne = lecture.ReadLine();
            }
            lecture.Close();
            for (int i = 0; i < ligne.Length; i++)
            {
                if (ligne[i] == ';')
                {
                    ligne = ligne.Remove(i, 1);
                }
            }
            this.de = ligne.ToCharArray();
            this.lettre = Lance(de, this.r);
        }

        /// <summary>
        /// Propriété qui représente la face qui figurera dans la grille
        /// </summary>
        public char FaceSup { get { return lettre; }}

        /// <summary>
        /// Propriété qui représente le numéro du dé considéré
        /// </summary>
        public int Numero { get { return numero; } }

        /// <summary>
        /// Méthode qui sélectionne par hasard la face du dé qui figurera dans la grille
        /// </summary>
        /// <param name="tab"> Le choix des caractères possibles pour ce dé </param>
        /// <returns> Retourne le caractère choisi </returns>
        public char Lance(char[] tab, Random r)
        {
            // numero de la face sup :  
            int numero = r.Next(1, 7);
            // la lettre équivalente est celle sur la face choisie de manière aléatoire: 
            return tab[numero - 1];
        }

        /// <summary>
        /// Méthode qui renvoie les informations relatives à un dé
        /// </summary>
        /// <returns> Returne une chaîne de caractère contenant l'ensemble des informations sur le dé </returns>
        public override string ToString()
        {
            // on affiche la liste de toutes les lettres des faces du dé 
            string liste = "";
            for (int i = 0; i <= de.Length; i++)
            {
                liste += de[i] + " ";
            }
            // on affiche quelles sont les différentes faces et laquelle est la supérieure 
            return (" les lettres des 6 faces du dé sont : " + liste + " et la face supéreieure est " + lettre);
        }

    }
}

