using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TicTacToe.Models;
using TicTacToe.Container;
using System.Web.Mvc;
using System.Collections.Generic;
using AutoMapper;


namespace TicTacToe.Controllers.Tests
{
    [TestClass()]
    public class GameControllerTests
    {
        public IRepository MoqRepository;
        public IMapper Mapper;
        public GameController gameController;

        public GameControllerTests()
        {
            IocContainer.Setup();
            MoqRepository = IocContainer.container.Resolve<IRepository>("MoqRepository");
            Mapper = IocContainer.container.Resolve<IMapper>();
            gameController = new GameController(MoqRepository, Mapper);
        }
        
        [TestMethod()]
        public void GameCreate_GetQuery_AlwaysReturnAllLevels()
        {           
            int countLevels=MoqRepository.GetLevelList().Count();

            ViewResult result = gameController.Create() as ViewResult;
            List<Level> levels = ((List<Level>)(result.ViewData["Levels"]));

            Assert.AreEqual(levels.Count(), countLevels);
        }

        [TestMethod()]
        public void GameCreate_PostQuery_WhenModelValid_ReturnRedirect()
        {
            Game game = new Game();
            game.PlayerName = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            
            var result = gameController.Create(game) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GameCreate_PostQuery_WhenModelNotValid_ReturnNotValidGame()
        {
            Game game = new Game();
            game.PlayerName = "";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            gameController.ModelState.AddModelError("Name", "Name is empty");

            ViewResult result = gameController.Create(game) as ViewResult;

            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.Model, game);
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenGameExistInRepo_ReturnNotNullModel()
        {
            Game game = new Game();
            game.Id = 1;
            game.PlayerName = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            MoqRepository.AddGame(game);

            ViewResult result = gameController.Battle(game.Id) as ViewResult;
            Assert.IsNotNull(result.Model);            
           
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenParamGameIdIsNull_ReturnRedirect()
        {
           int? gameId = null;
           var result = gameController.Battle(gameId) as RedirectToRouteResult;

           Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void Battle_GetQuery_WhenGameNotExistInRepo_ReturnRedirect()
        {
            int gameId = 99999999;
            var result = gameController.Battle(gameId) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenPlayerPlayForO_ReturnNewFieldWithStepOfComputer()
        {
            Game game = new Game();
            game.Id = 1;
            game.PlayerName = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 2;
            MoqRepository.AddGame(game);
            
            ViewResult result = gameController.Battle(game.Id) as ViewResult;
            Fields fields = Mapper.Map<DtoFields, Fields>((DtoFields)result.Model);

            Assert.IsNotNull(fields.FieldsStringArray.Contains("O"));
            Assert.AreEqual(fields.NumFreeFields.Count(), 8);
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenPlayerPlayForX_ReturnNewEmptyFields()
        {
            Game game = new Game();
            game.Id = 1;
            game.PlayerName = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            MoqRepository.AddGame(game);

            ViewResult result = gameController.Battle(game.Id) as ViewResult;
            Fields fields = Mapper.Map<DtoFields, Fields>((DtoFields)result.Model);
                        
            Assert.AreEqual(fields.NumFreeFields.Count(), 9);
        }

        [TestMethod()]
        public void Battle_PostQuery_WhenLastStepBeforeDrawSituation_ReturnStringDraw()
        {
            Game game = new Game();
            game.Id = 1;
            game.PlayerName = "Player";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            MoqRepository.AddGame(game);

            DtoFields dtoFields = new DtoFields();
            dtoFields.GameId = 1;           
            dtoFields.f1 = "X";            
            dtoFields.f3 = "X";
            dtoFields.f4 = "O";
            dtoFields.f5 = "X";
            dtoFields.f6 = "O";
            dtoFields.f7 = "O";
            dtoFields.f8 = "X";
            dtoFields.f9 = "O";
                      
            ViewResult result = gameController.Battle(dtoFields) as ViewResult;
            Assert.AreEqual(result.ViewData["Message"], "Ничья");

        }

        [TestMethod()]
        public void Battle_PostQuery_WhenGameNotEnd_ReturnFieldsCountPlus1()
        {
            Game game = new Game();
            game.Id = 1;
            game.PlayerName = "";
            game.LevelId = 1;
            game.PlayerTeamId = 1;
            MoqRepository.AddGame(game);

            DtoFields dtoFields = new DtoFields();
            dtoFields.GameId = 1;            
            dtoFields.f1 = "X"; 
            dtoFields.f8 = "O";
            int countFreeFieldsBefore = 7;

            ViewResult result = gameController.Battle(dtoFields) as ViewResult;

            Fields fields = Mapper.Map<DtoFields, Fields>((DtoFields)result.Model);
            int countFreeFieldsAfter = fields.NumFreeFields.Count();

            Assert.AreEqual(countFreeFieldsAfter, countFreeFieldsBefore - 1);
        }

        
    }
}