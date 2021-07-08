using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boogle;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Boogle.Tests
{
    [TestClass()]
    public class DictionnaireTests
    {
        [TestMethod()]
        public void toStringTest()
        {
            Dictionnaire Dico = new Dictionnaire("francais");
            Assert.AreEqual("Langue : francais" + '\n' + "Nombre de mots de 2 lettres : 75" + '\n' + "Nombre de mots de 3 lettres : 566" + '\n' + "Nombre de mots de 4 lettres : 1517" + '\n' + "Nombre de mots de 5 lettres : 4326" + '\n' + "Nombre de mots de 6 lettres : 7773" + '\n' + "Nombre de mots de 7 lettres : 12826" + '\n' + "Nombre de mots de 8 lettres : 18059" + '\n' + "Nombre de mots de 9 lettres : 20576" + '\n' + "Nombre de mots de 10 lettres : 20516" + '\n' + "Nombre de mots de 11 lettres : 17156" + '\n' + "Nombre de mots de 12 lettres : 12743" + '\n' + "Nombre de mots de 13 lettres : 7844" + '\n' + "Nombre de mots de 14 lettres : 4415" + '\n' + "Nombre de mots de 15 lettres : 2165", Dico.toString());
        }
    }
}