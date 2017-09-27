using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace TicTacToe.Models.Tests
{
    [TestClass()]
    public class TestFields
    {
        [TestMethod()]
        public void CountFields_ReturnSameCountAsCountProperties()
        {
            Fields fields = new Fields();
            var countFields = fields.CountFields();
            Assert.AreEqual(countFields, 9);
        }

        [TestMethod()]
        public void FieldsStringArray_ReturnSameStringArrayAsProperties()
        {
            string[] arrXO = new string[9];
            Fields fields = new Fields();
            fields.f1 = "X";
            fields.f3 = "O";
            fields.f8 = "X";

            arrXO[0] = "X";
            arrXO[2] = "O";
            arrXO[7] = "X";           
          
            CollectionAssert.AreEqual(arrXO, fields.FieldsStringArray);
        }
        
        [TestMethod()]
        public void SetValue_SetValueInPropertyOnIndex_ReturnFieldsWithSetProperty()
        {
            Fields testfields = new Fields();
            testfields.f1 = "X";
            
            Fields fields = new Fields();
            fields = fields.SetValue(0, fields, "X");
            Assert.AreEqual(testfields.f1, fields.f1);
            CollectionAssert.AreEqual(testfields.NumFreeFields, fields.NumFreeFields);
        }

        [TestMethod()]
        public void NumFreeFields_ReturnCorrectlyNumFreeFields()
        {
            List<int> numFreeFields = new List<int>();
            Fields fields = new Fields();
            fields.f1 = "X";
            fields.f3 = "O";
            fields.f8 = "X";

            numFreeFields.Add(1);
            numFreeFields.Add(3);
            numFreeFields.Add(4);
            numFreeFields.Add(5);
            numFreeFields.Add(6);
            numFreeFields.Add(8);           
            CollectionAssert.AreEqual(numFreeFields, fields.NumFreeFields);
        }
    }
       
}