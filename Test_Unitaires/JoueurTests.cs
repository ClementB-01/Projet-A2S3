using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boogle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boogle.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        [TestMethod()]
        public void JoueurTest()
        {
            Joueur Mathilde = new Joueur("Mathilde", 4);
            Assert.AreEqual("Mathilde", Mathilde.Nom);
            Assert.AreEqual(4, Mathilde.Score);
        }

        [TestMethod()]
        public void ContainTest()
        {
            Joueur Mathilde = new Joueur("Mathilde", 0);
            Mathilde.Add_Mot("Clément");
            Assert.AreEqual(false, Mathilde.Contain("Clément"));
        }

        [TestMethod()]
        public void Add_MotTest()
        {
            Joueur Mathilde = new Joueur("Mathilde", 0);
            Mathilde.Add_Mot("bonjour");
            Assert.AreEqual(false, Mathilde.Contain("bonjour"));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Joueur Mathilde = new Joueur("Mathilde", 6);
            Mathilde.Add_Mot("bonjour");
            Mathilde.Add_Mot("radar");
            //return (nom + " avec un score de " + score + " les mots qu'il a trouvé sont : " + ensemblemot);
            Assert.AreEqual("Mathilde" + " avec un score de " + "6" + " les mots qu'il a trouvé sont : " + "bonjour radar ", Mathilde.ToString());
        }
    }
}