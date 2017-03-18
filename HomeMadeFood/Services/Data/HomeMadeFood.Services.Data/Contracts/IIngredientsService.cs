using System;
using System.Collections.Generic;

using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IIngredientsService
    {
        IEnumerable<Ingredient> GetAllIngredients();

        IEnumerable<Ingredient> GetAllIngredientsIncludingRecipes();

        void AddIngredient(string name, Guid foodCategoryId, decimal pricePerMeasuringUnit, double quantityPerMeasuringUnit);

        Ingredient GetIngredientById(Guid id);

        void EditIngredient(Ingredient ingredient);

        void DeleteIngredient(Ingredient ingredient);
    }
}