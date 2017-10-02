using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {        
        public int Id { get; set; }

        [Display(Name = "Имя")]        
        public string PlayerName { get; set; }

        [Display(Name = "За кого играете")]        
        public int PlayerTeamId { get; set; }

        
        [Display(Name = "Уровень сложности ")]
        public int LevelId { get; set; }
        public virtual Level Level { get; set; }

        [Display(Name = "Победитель")]
        public string WhoWin { get; set; }

        public virtual List<Fields> Fields { get; set; }

        public string PlayerTeamName
        {
            get { return PlayerTeamId == 1 ? "X" : "O"; }
        }

    }
}