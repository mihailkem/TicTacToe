using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TicTacToe.Models;
using TicTacToe.GameRepository;
using TicTacToe.Container;
using System.Web.Mvc;
using System.Collections.Generic;

namespace TicTacToe.Controllers.Tests
{
    [TestClass()]
    public class GameControllerTests
    {
        public IRepository MoqRepository;
        public GameController g;

        public GameControllerTests()
        {
            IocContainer.Setup();
            MoqRepository = IocContainer.container.Resolve<IRepository>("MoqRepository");
            g = new GameController(MoqRepository);
        }
        
        [TestMethod()]
        public void Create_GetQuery_AlwaysReturnAllLevels()
        {           
            int countLevels=MoqRepository.GetLevelList().Count();

            ViewResult result = g.Create() as ViewResult;
            List<Level> levels = ((List<Level>)(result.ViewData["Levels"]));

            Assert.AreEqual(levels.Count(), countLevels);
        }

        [TestMethod()]
        public void Create_PostQuery_WhenModelValid_ReturnRedirect()
        {
            Game game = new Game();
            game.Player = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
           
            var result = g.Create(game) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Create_PostQuery_WhenModelNotValid_ReturnNotValidGame()
        {
            Game game = new Game();
            game.Player = "";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            g.ModelState.AddModelError("Name", "Name is empty");

            ViewResult result = g.Create(game) as ViewResult;

            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.Model, game);
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenGameExistInRepo_ReturnNotNullModel()
        {
            Game game = new Game();
            game.Id = 1;
            game.Player = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            MoqRepository.AddGame(game);

            ViewResult result = g.Battle(game.Id) as ViewResult;
            Assert.IsNotNull(result.Model);            
           
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenGameNotExistInRepo_ReturnRedirect()
        {
           int? nullInt = null;
           var result = g.Battle(nullInt) as RedirectToRouteResult;

           Assert.IsNotNull(result);
        }
        
        [TestMethod()]
        public void Battle_GetQuery_WhenPlayerPlayForO_ReturnNewFieldWithStepOfComputer()
        {
            Game game = new Game();
            game.Id = 1;
            game.Player = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 2;
            MoqRepository.AddGame(game);

            Fields fields = new Fields();
            fields.GameId = 1;
            fields.Game = game;


            ViewResult result = g.Battle(game.Id) as ViewResult;

            /*Assert.IsNotNull(result.Model);
            Assert.AreEqual(((Fields)result.Model).GameId, game.Id);
            Assert.AreEqual(((Fields)result.Model).Game, game);*/
            Assert.AreEqual(((Fields)result.Model).NumFreeFields.Count(), 8);
        }

        [TestMethod()]
        public void Battle_PostQuery_WhenLastStepBeforeDrawSituation_ReturnStringDraw()
        {
            Game game = new Game();
            game.Id = 1;
            game.Player = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            MoqRepository.AddGame(game);

            Fields fields = new Fields();
            fields.GameId = 1;
            fields.Game = game;
            fields.f1 = "X";
            //fields.f2 = "X";
            fields.f3 = "X";
            fields.f4 = "O";
            fields.f5 = "X";
            fields.f6 = "O";
            fields.f7 = "O";
            fields.f8 = "X";
            fields.f9 = "O";
            ViewResult result = g.Battle(fields) as ViewResult;
            Assert.AreEqual(result.ViewData["Message"], "Ничья");

        }

        [TestMethod()]
        public void Battle_PostQuery_WhenGameNotEnd_ReturnFieldsCountPlus1()
        {
            Game game = new Game();
            game.Id = 1;
            game.Player = "";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            MoqRepository.AddGame(game);

            Fields fields = new Fields();
            fields.GameId = 1;
            fields.Game = game;
            fields.f1 = "X";
            //fields.f2 = "X";
            fields.f3 = "X";
            fields.f4 = "O";
            fields.f5 = "X";
            fields.f6 = "O";
            fields.f7 = "O";
            fields.f8 = "X";
            fields.f9 = "O";

            ViewResult result = g.Battle(fields) as ViewResult;
            
            Assert.AreEqual(((Fields)result.Model).CountFields(), 9);

        }

        
    }
}