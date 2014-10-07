using InventoryApp.Data.Models;
using InventoryApp.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Data.Repository
{
    public class IngredientRepository
    {
        //create an instance of the database
        InventoryDatabaseEntities _inventoryEntities = new InventoryDatabaseEntities();

        public IEnumerable<IngredientModel> GetIngredients()
        {
            //retrieve items from the ingredients table of the inventory, save as a list, and assign it to a var
            var ingredients = _inventoryEntities.Ingredients.AsEnumerable();

            //make the list it just retrieved to pass to the controller
            List<IngredientModel> ingredientsForController = new List<IngredientModel>();

            //loop through that list of ingredients
            foreach (Ingredient ingredient in ingredients)
            {
                //convert type from int to enum
                IngredientType ingredientParsed = (IngredientType)Enum.Parse(typeof(IngredientType), ingredient.type.ToString());

                //convert ingredient from database to a Model the view can use
                IngredientModel ingredientModel = new IngredientModel(ingredient.id, ingredient.name, ingredientParsed, ingredient.expirationDate, ingredient.placeOfPurchase, ingredient.notes);

                //add those new models to the list that will be sent to controller
                ingredientsForController.Add(ingredientModel);
            }
            //send the list back
            return ingredientsForController;
        }

        public IngredientModel GetIngredients(int id)
        {
            //get ingredient from the database
            Ingredient ingredient = _inventoryEntities.Ingredients
                                        .Include("IngredientQuantities")
                                        .FirstOrDefault(m => m.id == id);
            if (ingredient != null)
            {
                //convert data type Int to Enum for ingredient.type
                IngredientType ingredientParsed = (IngredientType)Enum.Parse(typeof(IngredientType), ingredient.type.ToString());

                //convert ingredient to IngredientModel for controller to use
                IngredientModel ingredientModel = new IngredientModel(ingredient.id, ingredient.name, ingredientParsed, ingredient.expirationDate, ingredient.placeOfPurchase, ingredient.notes);
                
                //convert ingredient quantities
                List<IngredientQuantityModel> ingredientQuantities = ingredient.IngredientQuantities
                                                .AsEnumerable()
                                                .Select(m => new IngredientQuantityModel(m.ID, m.IngredientID, m.Amount, m.Unit))
                                                .ToList();
                //pass ingredient quantities tot he ingredient
                ingredientModel.IngredientQuantities = ingredientQuantities;

                //return the IngredientModel
                return ingredientModel;
            }
            else
            {
                return null;
            }            
        }

        public IEnumerable<IngredientModel> SearchIngredient(string Name)
        {
            //get a list of movies matching by name
            var searchedIngredient = _inventoryEntities.Ingredients.Where(i => i.name.Contains(Name)); 

            //create matching list of movies to be sent back 
            List<IngredientModel> matchingList = new List<IngredientModel>();

            foreach (Ingredient ingredient in searchedIngredient)
            {
                //convert type int to Ienumerable
                IngredientType ingredientParsed = (IngredientType)Enum.Parse(typeof(IngredientType), ingredient.type.ToString());

                //convert ingredient from database to ingredientModel
                IngredientModel matchingIngredient = new IngredientModel(ingredient.id, ingredient.name, ingredientParsed, ingredient.expirationDate, ingredient.placeOfPurchase, ingredient.notes);

                matchingList.Add(matchingIngredient);
            }
            return matchingList;
        }

        public void Edit(IngredientModel ingredient)
        {
            //get ingredient from the repository based on its ID
            Ingredient editedIngredient = _inventoryEntities.Ingredients.FirstOrDefault(m => m.id == ingredient.ID);

            if (editedIngredient != null)
            {
                //update database properties
                editedIngredient.name = ingredient.Name.Trim();
                editedIngredient.type = (int)ingredient.Type;
                editedIngredient.placeOfPurchase = ingredient.PlaceOfPurchase.Trim();
                editedIngredient.expirationDate = ingredient.ExpirationDate;
                editedIngredient.notes = ingredient.Notes;

                _inventoryEntities.SaveChanges();
            }
        }

        public int Create(IngredientModel newIngredient)
        {
            //convert ingredientModel to an ingredient for the database
            Ingredient dbIngredient = new Ingredient();
            dbIngredient.id = newIngredient.ID;
            dbIngredient.type = (int)newIngredient.Type;
            dbIngredient.name = newIngredient.Name;
            dbIngredient.expirationDate = newIngredient.ExpirationDate;
            dbIngredient.placeOfPurchase = newIngredient.PlaceOfPurchase;
            dbIngredient.notes = newIngredient.Notes;

            _inventoryEntities.Ingredients.Add(dbIngredient);
            _inventoryEntities.SaveChanges();

            return newIngredient.ID;
        }

        public void CreateQuantity(IngredientQuantityModel newIngredientQuantity)
        {
            //convert ingredientQuantityModel to a quantity for the database
            IngredientQuantity dbIngredientQuantity = new IngredientQuantity();
            dbIngredientQuantity.ID = newIngredientQuantity.ID;
            dbIngredientQuantity.IngredientID = newIngredientQuantity.IngredientID;
            dbIngredientQuantity.Amount = newIngredientQuantity.Amount;
            dbIngredientQuantity.Unit = newIngredientQuantity.Unit;

            _inventoryEntities.IngredientQuantities.Add(dbIngredientQuantity);
            _inventoryEntities.SaveChanges();
        }

        public void Delete(int id)
        {
            Ingredient deletedIngredient = _inventoryEntities.Ingredients.FirstOrDefault(i => i.id == id);           
            if (deletedIngredient != null)
            {
                DeleteQuantity(id);
                _inventoryEntities.Ingredients.Remove(deletedIngredient);                
                _inventoryEntities.SaveChanges();
            }
        }

        public void DeleteQuantity(int id)
        {
            IngredientQuantity deletedIngredientQuantity = _inventoryEntities.IngredientQuantities.FirstOrDefault(q => q.IngredientID == id);
            if (deletedIngredientQuantity != null)
            {
                _inventoryEntities.IngredientQuantities.Remove(deletedIngredientQuantity);
                _inventoryEntities.SaveChanges();
            }
        }

        public int GetItemCount()
        {
            int itemCount = _inventoryEntities.Ingredients.Count();
            return itemCount;
        }
    }
}


