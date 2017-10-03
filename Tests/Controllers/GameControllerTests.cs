using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TicTacToe.Models;
using TicTacToe.Container;
using System.Web.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using System;

namespace TicTacToe.Controllers.Tests
{
    [TestClass()]
    public class GameControllerTests
    {       
        public IMapper Mapper;     

        public GameControllerTests()
        {
            IocContainer.Setup();           
            Mapper = IocContainer.container.Resolve<IMapper>();           
        }
        
        [TestMethod()]
        public void GameCreate_GetQuery_AlwaysReturnAllLevels()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.GetLevelList()).Returns(new List<Level>() { new Level()});
            var controller = new GameController(mockRepo.Object, Mapper);
          
            // Act
            ViewResult result = controller.Create() as ViewResult;           
            List<Level> levels = result.ViewData["Levels"] as List<Level>;

            // Assert
            Assert.AreEqual(levels.Count(), mockRepo.Object.GetLevelList().Count(), "Не все уровни сложности извлечены из БД");
        }

        [TestMethod()]
        public void GameCreate_PostQuery_WhenModelValid_ReturnRedirect()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();           
            var controller = new GameController(mockRepo.Object, Mapper);          
            controller.ModelState.Clear();
            Game game = new Game();

            // Act
            var result = controller.Create(game) as RedirectToRouteResult;
                        
            // Assert
            Assert.IsNotNull(result, "Не произошел Redirect");            
            mockRepo.Verify(a => a.AddGame(game), Times.Once, "Метод добавления игры в БД не выполнился");
        }

        [TestMethod()]
        public void GameCreate_PostQuery_WhenModelNotValid_ReturnNotValidGame()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);            
            Game game = new Game();
            controller.ModelState.AddModelError("Name", "Name is empty");

            // Act
            ViewResult result = controller.Create(game) as ViewResult;

            // Assert
            Assert.IsTrue(result.ViewData.ModelState.Count == 1, "Отсутствует ошибка валидации");
            Assert.AreSame(result.Model, game, "Вернулся не тот же самый экземпляр Игры");
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenGameExistInRepo_ReturnNotNullModel()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);
            mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>())).Returns(new Game());

            // Act
            ViewResult result = controller.Battle(1) as ViewResult;

            // Assert   
            Assert.IsNotNull(result.Model, "Существующая игра извлечена из БД, но модель DtoFields не дошла до View");            
           
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenParamGameIdIsNull_ReturnRedirect()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);
            int? gameId = null;

            // Act
            var result = controller.Battle(gameId) as RedirectToRouteResult;

            // Assert 
            Assert.IsNotNull(result, "Не произошел Redirect");
        }
        
        [TestMethod()]
        public void Battle_GetQuery_WhenGameNotExistInRepo_ReturnRedirect()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);
           
            Game game = null;
            mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>())).Returns(game);

            // Act
            RedirectToRouteResult result = controller.Battle(It.IsAny<int>()) as RedirectToRouteResult;

            // Assert 
            Assert.IsNotNull(result, "Не произошел Redirect");
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenPlayerPlayForO_ReturnNewFieldWithStepOfComputer()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);
            mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>())).Returns(new Game() { PlayerTeamId = 2 });
           
            // Act
            ViewResult result = controller.Battle(1) as ViewResult;
            Fields fields = Mapper.Map<DtoFields, Fields>((DtoFields)result.Model);

            // Assert 
            Assert.IsTrue(fields.FieldsStringArray.Contains("X"),"Игровое поле не содержит первого хода компьютера");
            Assert.AreEqual(fields.NumFreeFields.Count(), 8, "Игровое поле содержит больше чем одно занятое поле");
        }

        [TestMethod()]
        public void Battle_GetQuery_WhenPlayerPlayForX_ReturnNewEmptyFields()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);
            mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>())).Returns(new Game() { PlayerTeamId = 1 });
            
            // Act
            ViewResult result = controller.Battle(It.IsAny<int>()) as ViewResult;
            Fields fields = Mapper.Map<DtoFields, Fields>((DtoFields)result.Model);

            // Assert 
            Assert.IsNotNull(fields,"Игровое поле не создано");
            Assert.AreEqual(fields.NumFreeFields.Count(), 9, "Игровое поле создано, но оно не пустое");
        }

        [TestMethod()]
        public void Battle_PostQuery_WhenEndGame_ReturnViewBagMessageWhoWinNotEmpty()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);
            Game game = new Game();
            game.PlayerTeamId = 1;
            mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>())).Returns(game);
            
            DtoFields dtoFields = new DtoFields();                     
            dtoFields.f1 = "X";
            dtoFields.f2 = "X";
            dtoFields.f3 = "X";
            dtoFields.f4 = "O";
            dtoFields.f5 = "X";
            dtoFields.f6 = "O";
            dtoFields.f7 = "O";
            dtoFields.f8 = "X";
            dtoFields.f9 = "O";

            // Act
            ViewResult result = controller.Battle(dtoFields) as ViewResult;

            // Assert            
            Assert.IsNotNull((result.ViewData["Message"]), "Сообщение о конце игры пустое");
            Assert.IsNotNull(String.IsNullOrEmpty((result.ViewData["Message"]).ToString()),"Сообщение о конце игры пустое");
            mockRepo.Verify(a => a.UpdateGame(game), Times.Once,"Игра не обновилась в базе");

        }

        [TestMethod()]
        public void Battle_PostQuery_WhenGameNotEnd_ReturnFieldsFreeCountMinus1()
        {
            // Arrange
            var mockRepo = new Mock<IRepository>();
            var controller = new GameController(mockRepo.Object, Mapper);
            Game game = new Game();
            game.PlayerTeamId = 1;
            mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>())).Returns(game);            

            DtoFields dtoFields = new DtoFields();                      
            dtoFields.f1 = "X"; 
            dtoFields.f8 = "O";
            int countFreeFieldsBefore = 7;

            // Act
            ViewResult result = controller.Battle(dtoFields) as ViewResult;

            // Assert 
            Fields fields = Mapper.Map<DtoFields, Fields>((DtoFields)result.Model);
            int countFreeFieldsAfter = fields.NumFreeFields.Count();

            Assert.AreEqual(countFreeFieldsAfter, countFreeFieldsBefore - 1,"Компьютер не сделал ход");
            mockRepo.Verify(a => a.AddFields(It.IsAny<Fields>()), Times.AtLeastOnce, "Игровое поле не добавилось в базу");

        }


    }
}