using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Data.Models
{
    public class IngredientQuantityModel
    {
        public int ID { get; set; }
        public int IngredientID { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }

        public IngredientQuantityModel() { }
        public IngredientQuantityModel(int id, int ingredientID, double amount, string unit)
        {
            ID = id;
            IngredientID = ingredientID;
            Amount = amount;
            Unit = unit;
        }
    }
}
