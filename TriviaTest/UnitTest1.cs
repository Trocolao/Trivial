using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using Trivia;

namespace TriviaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HowManyPlayersTestMethod()
        {
            Game game = new Game();
            game.Add("carlos");
            game.Add("david");
            game.Add("eva");
            Assert.AreEqual(3, game.HowManyPlayers());
        }
        /* [TestMethod]
         public void CreateRockQuestionTestMethod()
         {
             Game game = new Game();

             Assert.AreEqual("Rock Question " + 1, game.CreateRockQuestion(1));

         }*/
        [TestMethod]
        public void IsplayableTestMethod()
        {
            Game game = new Game();
            Assert.IsFalse(game.IsPlayable());
            game.Add("carlos");
            game.Add("david");
            Assert.IsTrue(game.IsPlayable());
        }
        [TestMethod]
        public void AddTestMethod()
        {
            Game game = new Game();
            Assert.IsTrue(game.Add("carlos"));
        }
        [TestMethod]
        public void WasCorrectlyAnswered()
        {
            Game game = new Game();
            game.Add("carlos");
            game.Roll(4);
            Assert.IsTrue(game.WasCorrectlyAnswered());
        }
        [TestMethod]
        public void WrongAnswer()
        {
            Game game = new Game();
            game.Add("carlos");
            game.Roll(4);
            Assert.IsTrue(game.WrongAnswer());
        }
    }
}
