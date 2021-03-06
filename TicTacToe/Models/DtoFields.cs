﻿namespace TicTacToe.Models
{
    /// <summary>
    /// Класс игрового поля для передачи его во View.
    /// Создан чтобы в представлении не тянуть данные из таблиц Game и Level,
    /// а передевать все в одной моделе.
    /// </summary>
    public class DtoFields
    {
        public int Id { get; set; }
        public string f1 { get; set; }
        public string f2 { get; set; }
        public string f3 { get; set; }
        public string f4 { get; set; }
        public string f5 { get; set; }
        public string f6 { get; set; }
        public string f7 { get; set; }
        public string f8 { get; set; }
        public string f9 { get; set; }
        public int GameId { get; set; }
        public string PlayerName { get; set; }        
        public string LevelName { get; set; }
        public string PlayerTeamName { get; set; }
        public string PlayerTeamId { get; set; }
    }
}