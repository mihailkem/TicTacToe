﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Tests
{
    [TestClass()]
    public class LogicsTests
    {
        [TestMethod()]
        public void doStep_CountAfterStepPlusOne()
        {
            Game game = new Game();
            game.Id = 1;
            game.LevelId = 1;
            game.PlayerTeamId = 1;

            Fields fields = new Fields();            
            fields.GameId = 1;
            fields.Game = game;
            fields.f1 = "X";
            fields.f3 = "O";
            fields.f8 = "X";
            
            Assert.AreEqual(Logics.doStep(fields).NumFreeFields.Count(), 5);
        }

        [TestMethod()]
        public void doStep_ReturnNotNull()
        {
            Game game = new Game();
            game.Id = 1;
            game.LevelId = 1;
            game.PlayerTeamId = 1;

            Fields fields = new Fields();            
            fields.GameId = 1;
            fields.Game = game;

            fields.f1 = "X";
            fields.f3 = "O";
            fields.f8 = "X";

            var result = Logics.doStep(fields);
            Assert.IsNotNull(result);
        }
    }
}