using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicTacToe.Models;

namespace TicTacToe
{
    public class Logics
    {
        /// <summary>
        /// Рандомная логика компьютера
        /// </summary>
        /// <param name="_numFreeFields">Номера свободных ячеек</param>
        /// <param name="next">Номер клетки следующего хода в массиве</param>
        /// <returns>Номер клетки следующего хода в массиве</returns>
        public static int strategyRandom(List<int> _numFreeFields, out int next)
        {
            Random rnd = new Random();            
            return next = _numFreeFields[rnd.Next(_numFreeFields.Count)];
        }

        /// <summary>
        /// Умная логика компьютера
        /// </summary>
        /// <param name="fields">Игровое поле</param>
        /// <param name="playerTeamId">За кого играет игрок. 1-Х, 2-О</param>
        /// <param name="next">Номер клетки следующего хода в массиве</param>
        /// <returns>Номер клетки следующего хода в массиве</returns>        
        public static int strategyBrain(Fields fields, int playerTeamId, out int next)
        {
            string[] _arrXO = fields.FieldsStringArray;
            next = 0;

            switch (playerTeamId)//Если игрок играет за Х, то компьютер ставит О
            {
                //Алгоритм ходов ноликов
                case 1:                    
                    if (fields.NumFreeFields.Count() == 8)  //Первый ход
                    {
                        if (_arrXO[4] == null) return next = 4;                 //Если игрок Х не поставил Х в центральную клетку, то ставим туда О 
                        else return next = fields.GetRandomFreeCornerFields();  //Иначе ставим О в диагональные ячейки 0,2,6, или 8                   
                    }
                    else //Второй и последующие  ходы 
                    {
                        int winO = GetNextWhenTwoOfTheeeFull(fields, "O");
                        if (winO != -1) return next = winO;                         // Если нам остался один ход до выиграша, то делаем его!                      
                        else next = GetNextWhenTwoOfTheeeFull(fields, "X"); //иначе защищаемся, т.е. ищем ситуации когда Х остался один ход до выигрыша и ставим туда О

                        if (next == -1)//если нету такого состояния что один ход остался О или Х
                        {
                            if (_arrXO[4] == "O")  //если мы до этого поставили О в центр, то ищем возможность поставить О в неугловую ячеку                            
                                if (fields.GetRandomFreeNotCornerFields() != -1) next = fields.GetRandomFreeNotCornerFields();                            
                            else                   //иначе ищем возможность поставить О в угловую ячеку                            
                                if (fields.GetRandomFreeCornerFields() != -1) next = fields.GetRandomFreeCornerFields();                            
                        }

                        //Если после всех попыток не нашли куда поставить О, то ставим рандомно.
                        if (next == -1) next = fields.GetRandomFreeFields();

                    }   
                    break;
                case 2:
                    if (fields.NumFreeFields.Count() == 9) return next = 4;     //Первый ход, ставим в центр
                    else                                                        //Второй и последующие  ходы 
                    {
                        int winX = GetNextWhenTwoOfTheeeFull(fields, "X");
                        if (winX != -1) return next = winX;                         // Если нам остался один ход до выиграша, то делаем его!                      
                        else next = GetNextWhenTwoOfTheeeFull(fields, "O"); //иначе защищаемся, т.е. ищем ситуации когда Х остался один ход до выигрыша и ставим туда О

                        if (next == -1)//если нету такого состояния что один ход остался О или Х
                        {                           
                            //TODO надо выбирать нерандомно угол, а по умному
                                if (fields.GetRandomFreeCornerFields() != -1) next = fields.GetRandomFreeCornerFields();
                                else                   //иначе ищем возможность поставить О в угловую ячеку                            
                                if (fields.GetRandomFreeNotCornerFields() != -1) next = fields.GetRandomFreeNotCornerFields();
                        }
                    }
                    break;
                default:
                    break;
            }
        
            return next;

        }

        /// <summary>
        /// Метод возвращающий номер следующей ячеки в тех ситуациях когда от выигрыша или проигрыша остался один ход
        /// </summary>        
        /// <param name="fields">Ячейки игрового поля</param>
        /// <param name="checkValue">Проверяемое значение. Х или О</param>
        /// <returns></returns>
        private static int GetNextWhenTwoOfTheeeFull(Fields fields, string checkValue)
        {
            int next = -1;          
            {
                if (fields.FieldsStringArray[0] == checkValue & fields.FieldsStringArray[1] == checkValue & fields.NumFreeFields.Contains(2)) return next = 2;
                if (fields.FieldsStringArray[0] == checkValue & fields.FieldsStringArray[2] == checkValue & fields.NumFreeFields.Contains(1)) return next = 1;
                if (fields.FieldsStringArray[1] == checkValue & fields.FieldsStringArray[2] == checkValue & fields.NumFreeFields.Contains(0)) return next = 0;

                if (fields.FieldsStringArray[6] == checkValue & fields.FieldsStringArray[7] == checkValue & fields.NumFreeFields.Contains(8)) return next = 8;
                if (fields.FieldsStringArray[6] == checkValue & fields.FieldsStringArray[8] == checkValue & fields.NumFreeFields.Contains(7)) return next = 7;
                if (fields.FieldsStringArray[7] == checkValue & fields.FieldsStringArray[8] == checkValue & fields.NumFreeFields.Contains(6)) return next = 6;

                if (fields.FieldsStringArray[0] == checkValue & fields.FieldsStringArray[3] == checkValue & fields.NumFreeFields.Contains(6)) return next = 6;
                if (fields.FieldsStringArray[0] == checkValue & fields.FieldsStringArray[6] == checkValue & fields.NumFreeFields.Contains(3)) return next = 3;
                if (fields.FieldsStringArray[3] == checkValue & fields.FieldsStringArray[6] == checkValue & fields.NumFreeFields.Contains(0)) return next = 0;

                if (fields.FieldsStringArray[2] == checkValue & fields.FieldsStringArray[5] == checkValue & fields.NumFreeFields.Contains(8)) return next = 8;
                if (fields.FieldsStringArray[2] == checkValue & fields.FieldsStringArray[8] == checkValue & fields.NumFreeFields.Contains(5)) return next = 5;
                if (fields.FieldsStringArray[5] == checkValue & fields.FieldsStringArray[8] == checkValue & fields.NumFreeFields.Contains(2)) return next = 2;

                if (fields.FieldsStringArray[1] == checkValue & fields.FieldsStringArray[4] == checkValue & fields.NumFreeFields.Contains(7)) return next = 7;
                if (fields.FieldsStringArray[1] == checkValue & fields.FieldsStringArray[7] == checkValue & fields.NumFreeFields.Contains(4)) return next = 4;
                if (fields.FieldsStringArray[4] == checkValue & fields.FieldsStringArray[7] == checkValue & fields.NumFreeFields.Contains(1)) return next = 1;

                if (fields.FieldsStringArray[3] == checkValue & fields.FieldsStringArray[4] == checkValue & fields.NumFreeFields.Contains(5)) return next = 5;
                if (fields.FieldsStringArray[3] == checkValue & fields.FieldsStringArray[5] == checkValue & fields.NumFreeFields.Contains(4)) return next = 4;
                if (fields.FieldsStringArray[4] == checkValue & fields.FieldsStringArray[5] == checkValue & fields.NumFreeFields.Contains(3)) return next = 3;

                //диагонали
                if (fields.FieldsStringArray[0] == checkValue & fields.FieldsStringArray[4] == checkValue & fields.NumFreeFields.Contains(8)) return next = 8;
                if (fields.FieldsStringArray[0] == checkValue & fields.FieldsStringArray[8] == checkValue & fields.NumFreeFields.Contains(4)) return next = 4;
                if (fields.FieldsStringArray[4] == checkValue & fields.FieldsStringArray[8] == checkValue & fields.NumFreeFields.Contains(0)) return next = 0;

                if (fields.FieldsStringArray[2] == checkValue & fields.FieldsStringArray[4] == checkValue & fields.NumFreeFields.Contains(6)) return next = 6;
                if (fields.FieldsStringArray[2] == checkValue & fields.FieldsStringArray[6] == checkValue & fields.NumFreeFields.Contains(4)) return next = 4;
                if (fields.FieldsStringArray[4] == checkValue & fields.FieldsStringArray[6] == checkValue & fields.NumFreeFields.Contains(2)) return next = 2;


                //занятость всего угла
                if (fields.FieldsStringArray[1] == checkValue & fields.FieldsStringArray[5] == checkValue & fields.NumFreeFields.Contains(2)) return next = 2;
                if (fields.FieldsStringArray[5] == checkValue & fields.FieldsStringArray[7] == checkValue & fields.NumFreeFields.Contains(8)) return next = 8;
                if (fields.FieldsStringArray[7] == checkValue & fields.FieldsStringArray[3] == checkValue & fields.NumFreeFields.Contains(6)) return next = 6;
                if (fields.FieldsStringArray[3] == checkValue & fields.FieldsStringArray[1] == checkValue & fields.NumFreeFields.Contains(0)) return next = 0;
                               
                return next;
            }         
        }

        /// <summary>
        /// Метод определяющий в какую ячейку будет установлено значение на следующем ходу
        /// </summary>
        /// <param name="LevelId">Уровень сложности</param>
        /// <param name="playerTeamId">За кого играет игрок 1-Х, 2-О</param>
        /// <param name="fields">Игровое поле</param>
        /// <returns></returns>
        public static int nextStep(int LevelId, int playerTeamId ,Fields fields)
        {
            int next;
            switch (LevelId)
            {
                case 1:
                    return Logics.strategyRandom(fields.NumFreeFields, out next);
                case 2:
                    return Logics.strategyBrain(fields, playerTeamId, out next);
                default:
                    return Logics.strategyRandom(fields.NumFreeFields, out next);
            }

        }

        /// <summary>
        /// Проверка кто выиграл
        /// </summary>
        /// <param name="arr">Массив значений X или O</param>
        /// <returns>Возвращает строку с текстом о победе или проигрыше</returns>
        static public string whoWin(Fields fields)
        {
            string[] arr = fields.FieldsStringArray;
            if (arr[0] == "X" & arr[1] == "X" & arr[2] == "X" ||
                arr[3] == "X" & arr[4] == "X" & arr[5] == "X" ||
                arr[6] == "X" & arr[7] == "X" & arr[8] == "X" ||
                arr[0] == "X" & arr[3] == "X" & arr[6] == "X" ||
                arr[1] == "X" & arr[4] == "X" & arr[7] == "X" ||
                arr[2] == "X" & arr[5] == "X" & arr[8] == "X" ||
                arr[0] == "X" & arr[4] == "X" & arr[8] == "X" ||
                arr[2] == "X" & arr[4] == "X" & arr[6] == "X")
            {
                return "Выиграл X";
            }
            else if
               (arr[0] == "O" & arr[1] == "O" & arr[2] == "O" ||
                arr[3] == "O" & arr[4] == "O" & arr[5] == "O" ||
                arr[6] == "O" & arr[7] == "O" & arr[8] == "O" ||
                arr[0] == "O" & arr[3] == "O" & arr[6] == "O" ||
                arr[1] == "O" & arr[4] == "O" & arr[7] == "O" ||
                arr[2] == "O" & arr[5] == "O" & arr[8] == "O" ||
                arr[0] == "O" & arr[4] == "O" & arr[8] == "O" ||
                arr[2] == "O" & arr[4] == "O" & arr[6] == "O")
            {
                return "Выиграл O";
            }
            return "";
        }

        /// <summary>
        /// Функция делающая ход компьютера. Изменяет состояние ячеек игрового поля Fields.
        /// </summary>
        /// <param name="game">Игра</param>
        /// <param name="fields">Ячейки</param>
        /// <returns>Ячейки с одним измененым значением </returns>
        static public Fields doStep(Fields fields)
        {
            //Метод nextStep возвращает только номер ячейки
            //затем мы определяем какое значение ставить в эту ячейку
            //TODO возможно переписать чтобы метод возвращал сразу и то и то
            int next = nextStep(fields.Game.LevelId, fields.Game.PlayerTeamId, fields);
            string nameTeamComp = fields.Game.PlayerTeamId == 1 ? "O" : "X";
            fields = fields.SetValue(next, fields, nameTeamComp);
            return fields;
        }
        
    }
}