using HomeMadeFood.Models;
using System.Collections.Generic;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IRecipesService
    {
        void AddRecipe(Recipe recipe, IEnumerable<string> ingredientNames, IEnumerable<decimal> quantities);
    }
}
