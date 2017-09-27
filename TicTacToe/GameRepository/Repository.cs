using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TicTacToe.Models;
using static TicTacToe.Models.Fields;

namespace TicTacToe.GameRepository
{   

    public class Repository : IRepository
    {        
        private readonly TicTacToeContext _Context;        

        public Repository(TicTacToeContext Context)
        {           
            this._Context = Context;
        }

        #region Game
        public void AddGame(Game game)
        {            
            _Context.Games.Add(game);
            _Context.SaveChanges();            
        }

        public Game GetGameById(int id)
        {           
            return _Context.Games.Where(x => x.Id == id).Include(a => a.Level).FirstOrDefault();
         }

        public void UpdateGame(Game game)
        {            
            _Context.Entry(game).State = EntityState.Modified;
            _Context.SaveChanges(); 
        }
        #endregion
        
        
        public void AddFields(Fields fields)
        {            
            _Context.Fields.Add(fields);
            _Context.SaveChanges();
        }       
        
        
        public IEnumerable<Level> GetLevelList()
        {            
            return _Context.Levels.ToList();
        }

        public IEnumerable<Game> GetGamesList()
        {
            return _Context.Games.ToList();
        }
    }
}