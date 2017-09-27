using System;
using System.Web.Mvc;
using TicTacToe.Models;
using System.Web.WebPages;

namespace TicTacToe.Controllers
{
    /// <summary>
    /// Класс создания новой игры
    /// </summary>
    public class GameController : Controller
    {
        private readonly IRepository _Repository;
        
        public GameController(IRepository repository)
        {
            _Repository = repository;            
        }


        public ActionResult Create()
        {            
            ViewBag.Levels = _Repository.GetLevelList();           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game game)
        {            
                if (ModelState.IsValid)
                {
                    _Repository.AddGame(game);
                    return RedirectToAction("Battle", new { gameId = game.Id });
                }
            
            return View(game);
        }

        //Есть созданая игра, делаем первый ход
        public ActionResult Battle(int? gameId)
        {
            if (gameId == null)
                return RedirectToAction("Create", "Game");

            Game game = _Repository.GetGameById((int)gameId);
            Fields fields = new Fields{ Game = game, GameId = game.Id };
                        
            //если играем за "О", то комп делает первый шаг
            if (game.PlayerTeamId == 2)
            {                
                fields = Logics.doStep(fields);                
                _Repository.AddFields(fields);                
            }         
            
            return View(fields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Battle(Fields fields)
        {
            _Repository.AddFields(fields);
            Game game = _Repository.GetGameById(fields.GameId);

            string _whoWin = Logics.whoWin(fields);
            if (!String.IsNullOrEmpty(_whoWin))
            {
                ViewBag.Message = _whoWin;
            }
            else if (fields.NumFreeFields.Count > 0) //Если игра не окончена, есть свободная ячейка, то комп делает ход
            {                
                fields = Logics.doStep(fields);
                _Repository.AddFields(fields);  
                ViewBag.Message = Logics.whoWin(fields);
            }

            //если все клетки заняты и никто не выиграл
            if (fields.NumFreeFields.Count == 0 && Logics.whoWin(fields) == "") 
                 ViewBag.Message = "Ничья";           


            if (ViewBag.Message != "")
            {                
                game.WhoWin = ViewBag.Message;
                _Repository.UpdateGame(game);
            }

            if (fields.Game == null) fields.Game = game;
            return View(fields);
        }
        

    }
}