﻿using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Models
{
    /// <summary>
    /// Уровень сложности
    /// </summary>
    public class Level
    {       
        public int Id { get; set; }

        [Display(Name = "Уровень сложности")]
        public string LevelName { get; set; }
    }
}