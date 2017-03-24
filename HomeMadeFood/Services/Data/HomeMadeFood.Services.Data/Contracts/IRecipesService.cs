using System;
using System.Collections.Generic;

using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IRecipesService
    {
        void AddRecipe(Recipe recipe, 
            IEnumerable<string> ingredientNames, 
            IEnumerable<double> quantities, 
            IEnumerable<decimal> ingredientPrices,
            IEnumerable<Guid> foodCategories);

        IEnumerable<Recipe> GetAllRecipes();

        void EditRecipe(Recipe recipe);

        void DeleteRecipe(Recipe recipe);

        Recipe GetRecipeById(Guid id);

        IEnumerable<Recipe> GetAllOfDishType(DishType dishType);
    }
}