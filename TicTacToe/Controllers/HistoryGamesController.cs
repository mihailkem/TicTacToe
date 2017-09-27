using System.Net;
using System.Web.Mvc;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class HistoryGamesController : Controller
    {

        private readonly IRepository _Repository;

        public HistoryGamesController(IRepository repository)
        {
            _Repository = repository;
        }               

        // GET: Games
         public ActionResult ListGames()
         {          
             return View(_Repository.GetGamesList());
         }

        // GET: Games/Details/5
         public ActionResult Details(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.NotFound);
             }
             Game game = _Repository.GetGameById((int)id);
            
             if (game == null)
             {
                 return HttpNotFound();
             }
             return View(game);
         }
       
    }
}
