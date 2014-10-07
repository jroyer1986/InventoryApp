using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Data.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Data.Models
{
    public class IngredientModel
    {
        #region Properties

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public IngredientType Type { get; set; }
        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }
        [DisplayName("Place of Purchase")]
        public string PlaceOfPurchase { get; set; }
        public string Notes { get; set; }

        public List<IngredientQuantityModel> IngredientQuantities { get; set; }
        #endregion
 

        #region Constructor
        public IngredientModel(int id, string name, IngredientType type, DateTime? expirationDate, string placeOfPurchase, string notes) {
            ID = id;
            Name = name;
            Type = type;
            ExpirationDate = expirationDate;
            PlaceOfPurchase = placeOfPurchase;
            Notes = notes;
        }
        public IngredientModel() { }
        #endregion
    }
}
