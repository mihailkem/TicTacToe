using System;
using System.Web.Mvc;
using TicTacToe.Models;
using AutoMapper;

namespace TicTacToe.Controllers
{
    /// <summary>
    /// Класс создания новой игры
    /// </summary>
    public class GameController : Controller
    {
        private readonly IRepository _Repository;
        private readonly IMapper _Mapper;

        public GameController(IRepository repository,IMapper mapper)
        {
            _Repository = repository;
            _Mapper = mapper;
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
            if (game == null)
                return RedirectToAction("Create", "Game");

            Fields fields = new Fields{ Game = game, GameId = game.Id };
                        
            //если играем за "О", то комп делает первый шаг
            if (game.PlayerTeamId == 2)
            {                
                fields = Logics.doStep(fields);                
                _Repository.AddFields(fields);                
            }
                        
            DtoFields dtoFields = _Mapper.Map<Fields, DtoFields>(fields);
            return View(dtoFields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Battle(DtoFields dtoFields)
        {            
            Fields fields = _Mapper.Map<DtoFields, Fields>(dtoFields);

            _Repository.AddFields(fields);
            Game game = _Repository.GetGameById(fields.GameId);

            fields.Game = game;

            string _whoWin = Logics.whoWin(fields);
            if (!String.IsNullOrEmpty(_whoWin))
            {
                ViewBag.Message = _whoWin;
            }
            //Если игра не окончена, есть свободная ячейка, то комп делает ход
            else if (fields.NumFreeFields.Count > 0)            
            {                
                fields = Logics.doStep(fields);
                _Repository.AddFields(fields);  
                ViewBag.Message = Logics.whoWin(fields);
            }

            //если все клетки заняты и никто не выиграл
            if (fields.NumFreeFields.Count == 0 && Logics.whoWin(fields) == "") 
                 ViewBag.Message = "Ничья"; 

            if (!String.IsNullOrEmpty(ViewBag.Message))
            {                
                game.WhoWin = ViewBag.Message;
                _Repository.UpdateGame(game);
            }
                                    
            dtoFields = _Mapper.Map<Fields, DtoFields>(fields);

            return View(dtoFields);
        }
        

    }
}