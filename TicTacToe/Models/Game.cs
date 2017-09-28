using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required]
        public string PlayerName { get; set; }

        [Display(Name = "За кого играете")]
        [Required]
        public int PlayerTeamId { get; set; }

        [ForeignKey("Level")]
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