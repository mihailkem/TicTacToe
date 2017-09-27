using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToe.Models;

namespace TicTacToe.GameRepository
{
    public class MoqRepository : IRepository
    {       
        List<Game> Games = new List<Game>();
        List<Fields> Fields = new List<Fields>();

        public void AddFields(Fields fields)
        {
            Fields.Add(fields);
        }

        public void AddGame(Game game)
        {
            Games.Add(game);            
        }

        public Game GetGameById(int id)
        {
            return Games.Find(a => a.Id == id);
        }

        public IEnumerable<Level> GetLevelList()
        {
            List<Level> levels = new List<Level>();
            levels.Add(new Level() { Id = 1, LevelName = "Easy"});
            levels.Add(new Level() { Id = 2, LevelName = "Hard" });
            return levels;            
        }

        public void UpdateGame(Game game)
        {
            Game _game = Games.Find(a => a.Id == game.Id);
            Games.Remove(_game);
            Games.Add(game);           
        }

        public IEnumerable<Game> GetGamesList()
        {
            return Games.ToList();
        }
    }
}