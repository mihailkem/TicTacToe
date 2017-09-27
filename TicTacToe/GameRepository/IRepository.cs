using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TicTacToe.Models;

namespace TicTacToe
{    
    public interface IRepository
    {
        void AddGame(Game game);
        Game GetGameById(int id);
        void UpdateGame(Game game);
        void AddFields(Fields fields);
        IEnumerable<Level> GetLevelList();
        IEnumerable<Game> GetGamesList();
    }
}