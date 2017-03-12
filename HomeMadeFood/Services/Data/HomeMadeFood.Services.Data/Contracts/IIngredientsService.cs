using HomeMadeFood.Models;
using HomeMadeFood.Models.Enums;
using System.Collections.Generic;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IIngredientsService
    {
        IEnumerable<Ingredient> GetAllIngredients();

        void AddIngredient(string name, FoodType foodType, decimal pricePerMeasuringUnit, MeasuringUnitType measuringUnit, decimal quantity = 0);
    }
}