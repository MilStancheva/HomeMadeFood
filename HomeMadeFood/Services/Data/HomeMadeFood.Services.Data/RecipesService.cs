using Bytes2you.Validation;
using HomeMadeFood.Data.Data;
using HomeMadeFood.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data
{
    public class RecipesService : IRecipesService
    {
        private readonly IHomeMadeFoodData data;
        public RecipesService(IHomeMadeFoodData data)
        {
            Guard.WhenArgument(data, "data").IsNull().Throw();
            this.data = data;
        }

        public void AddRecipe(Recipe recipe, IEnumerable<string> ingredientNames, IEnumerable<decimal> ingredientQuantities)
        {
            //Guard.WhenArgument(recipe, "recipe").IsNull().Throw();

            //var ingredients = new List<Ingredient>();
            //var ingredientsAsList = ingredientNames.ToList();
            //var quantitiesAsList = ingredientQuantities.ToList();
            //var count = ingredientsAsList.Count;

            //for (int i = 0; i < count; i++)
            //{
            //    var name = ingredientsAsList[i].ToLower();
            //    var existingIngredient = this.data.Ingredients.GetAll().FirstOrDefault(x => x.Name.ToLower() == name);
            //    existingIngredient.Quantity += quantitiesAsList[i];
            //    recipe.Ingredients.Add(existingIngredient);
            //}

            //this.data.Recipes.Add(recipe);
            //this.data.Commit();
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return this.data.Recipes.GetAll().OrderBy(x => x.Id);
        }
    }
}
