﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    /// <summary>
    /// Игровое поле
    /// </summary>
    public class Fields
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
        public virtual Game Game { get; set; }

        /// <summary>
        /// Массив всех свойств класса начинающихся на букву "f", т.е. поля игрового поля
        /// </summary>
        private PropertyInfo[] PropertyStartWithF
        {
            get { return typeof(Fields).GetProperties().Where(x => x.Name.Substring(0, 1) == "f").ToArray(); }
        }

        /// <summary>
        /// Кол-во ячеек игрового поля
        /// </summary>
        /// <returns></returns>
        public int CountFields()
        {
            return PropertyStartWithF.Count();
        }

        /// <summary>
        /// Устанавливает значение в ячейку игрового поля по его индексу.
        /// </summary>
        /// <param name="numFileds">Номер ячейки. От 0 до 8</param>
        /// <param name="fields">Игровое поле в котором надо сделать изменение</param>
        /// <param name="valueTeam">Значение, которое надо установить. Х или О</param>
        /// <returns>Измененное игровое поле</returns>
        public Fields SetValue(int numFileds, Fields fields, string valueTeam)
        {
            if (numFileds >= 0 && numFileds <= 8)
                typeof(Fields).GetProperties().FirstOrDefault(x => x.Name == "f" + (numFileds + 1).ToString()).SetValue(fields, valueTeam);
            return fields;
        }

        /// <summary>
        /// String Массив ячеек игрового поля
        /// </summary>
        public string[] FieldsStringArray
        {
            get
            {
                int _countFields = CountFields();
                string[] arrXO = new string[_countFields];

                for (int i = 0; i < _countFields; i++)
                {
                    var _valueProperty = PropertyStartWithF[i].GetValue(this);
                    if (_valueProperty != null)
                        arrXO[i] = _valueProperty.ToString();
                }
                return arrXO;
            }
        }

        /// <summary>
        /// Список номеров свободных ячеек игрового поля.Значения начинаются с 0
        /// </summary>       
        public List<int> NumFreeFields
        {
            get
            {
                List<int> numFreeFields = new List<int>();//номера свободных ячеек
                for (int i = 0; i < FieldsStringArray.Length; i++)
                    if (FieldsStringArray[i] == null) numFreeFields.Add(i);
                return numFreeFields;
            }
        }

        /// <summary>
        /// Список свободных угловых ячеек
        /// </summary>
        public List<int> FreeCornerFields
        {
            get
            {
                List<int> targetFields = new List<int>();
                if (NumFreeFields.Contains(0)) targetFields.Add(0);
                if (NumFreeFields.Contains(2)) targetFields.Add(2);
                if (NumFreeFields.Contains(6)) targetFields.Add(6);
                if (NumFreeFields.Contains(8)) targetFields.Add(8);
                return targetFields;
            }
        }

        /// <summary>
        /// Список свободных НЕугловых ячеек
        /// </summary>
        public List<int> FreeNotCornerFields
        {
            get
            {
                List<int> targetFields = new List<int>();
                if (NumFreeFields.Contains(1)) targetFields.Add(1);
                if (NumFreeFields.Contains(3)) targetFields.Add(3);
                if (NumFreeFields.Contains(5)) targetFields.Add(5);
                if (NumFreeFields.Contains(7)) targetFields.Add(7);
                return targetFields;
            }
        }

        /// <summary>
        /// Получить номер рандомной свободной ячейки
        /// </summary>        
        public int GetRandomFreeFields()
        {
            Random rnd = new Random();
            return NumFreeFields[rnd.Next(NumFreeFields.Count())];
        }

        /// <summary>
        /// Получить номер рандомной свободной угловой ячейки
        /// </summary>        
        public int GetRandomFreeCornerFields()
        {
            if (FreeCornerFields.Count > 0)
            {
                Random rnd = new Random();
                return FreeCornerFields[rnd.Next(FreeCornerFields.Count)];
            }
            return -1;
        }

        /// <summary>
        /// Получить номер рандомной свободной НЕугловой ячейки
        /// </summary>       
        public int GetRandomFreeNotCornerFields()
        {
            if (FreeNotCornerFields.Count > 0)
            {
                Random rnd = new Random();
                return FreeNotCornerFields[rnd.Next(FreeNotCornerFields.Count)];
            }
            return -1;
        }
    }
}