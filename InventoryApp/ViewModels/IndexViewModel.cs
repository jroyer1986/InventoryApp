using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryApp.Data.Models;

namespace InventoryApp.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<IngredientModel> IngredientList
        {
            get;
            set;
        }
        
        #region Constructor

        public IndexViewModel() { }
        public IndexViewModel(IEnumerable<IngredientModel> ingredients)
        {
            IngredientList = ingredients;
        }

        #endregion
    }
}