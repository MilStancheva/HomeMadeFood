using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using System;
using System.Collections.Generic;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IIngredientsService
    {
        IEnumerable<Ingredient> GetAllIngredients();

        void AddIngredient(string name, FoodType foodType, decimal pricePerMeasuringUnit, MeasuringUnitType measuringUnit);

        Ingredient GetIngredientById(Guid id);

        void EditIngredient(Ingredient ingredient);

        void DeleteIngredient(Ingredient ingredient);
    }
}