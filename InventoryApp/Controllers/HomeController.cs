using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryApp.Data.Models;
using InventoryApp.Data.Repository;
using InventoryApp.ViewModels;

namespace InventoryApp.Controllers
{
    public class HomeController : Controller
    {
       IngredientRepository _ingredientRepository = new IngredientRepository();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //get a list of ingredients from repository
            IEnumerable<IngredientModel> ingredients = _ingredientRepository.GetIngredients();

            IndexViewModel viewIndex = new IndexViewModel(ingredients);
            return View(viewIndex);
        }

        public ActionResult Details(int id) 
        {
            IngredientModel ingredient = _ingredientRepository.GetIngredients(id);
            if(ingredient != null)
            {
                return View(ingredient);
            }
            else
            {
                ViewBag.ErrorMessage = "That Ingredient Does Not Exist!";
                return View("Error");
            }            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            IngredientModel ingredient = _ingredientRepository.GetIngredients(id);
            if (ingredient != null)
            {
                return View(ingredient);    
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Search (string Name)
        {
            IEnumerable<IngredientModel> searchedIngredients = _ingredientRepository.SearchIngredient(Name);

            IndexViewModel searchIndex = new IndexViewModel(searchedIngredients);
            return View("Index", searchIndex);
        }

        public ActionResult Edit(IngredientModel updatedIngredient)
        {
            _ingredientRepository.Edit(updatedIngredient);
            return RedirectToAction("Details", new { id = updatedIngredient.ID });
        }

        [HttpGet]
        public ActionResult Create()
        {
            IngredientModel ingredient = new IngredientModel();

            return View(ingredient);
        }

        [HttpPost]
        public ActionResult Create(IngredientModel newIngredient)
        {
            _ingredientRepository.Create(newIngredient);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {             
            _ingredientRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteOneQuantity(int id)
        {
            var itemToDelete= _ingredientRepository.GetIngredientQuantity(id);
            _ingredientRepository.DeleteOneQuantity(id);
            return RedirectToAction("Details", new { id = itemToDelete.IngredientID});
        }

        [HttpGet]
        public ActionResult CreateQuantity(int ingredientID)
        {
            IngredientQuantityModel quantity = new IngredientQuantityModel();

            quantity.IngredientID = ingredientID;

            return View(quantity);
        }

        [HttpPost]
        public ActionResult CreateQuantity(IngredientQuantityModel newIngredientQuantity)
        {
            _ingredientRepository.CreateQuantity(newIngredientQuantity);
            return RedirectToAction("Details", new {id = newIngredientQuantity.IngredientID});
        }

        public int GetItemCount()
        {
           return  _ingredientRepository.GetItemCount();
        }
    }
}
