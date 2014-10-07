//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryApp.Data.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingredient
    {
        public Ingredient()
        {
            this.IngredientQuantities = new HashSet<IngredientQuantity>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public Nullable<System.DateTime> expirationDate { get; set; }
        public string placeOfPurchase { get; set; }
        public string notes { get; set; }
    
        public virtual ICollection<IngredientQuantity> IngredientQuantities { get; set; }
    }
}
