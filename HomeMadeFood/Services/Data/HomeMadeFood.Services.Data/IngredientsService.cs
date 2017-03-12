using Bytes2you.Validation;
using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeMadeFood.Models.Enums;

namespace HomeMadeFood.Services.Data
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IHomeMadeFoodData data;
        public IngredientsService(IHomeMadeFoodData data)
        {
            Guard.WhenArgument(data, "data").IsNull().Throw();
            this.data = data;
        }

        public void AddIngredient(string name, FoodType foodType, decimal pricePerMeasuringUnit, MeasuringUnitType measuringUnit, decimal quantity = 0)
        {
            Ingredient ingredient = new Ingredient()
            {
                Name = name,
                FoodType = foodType,
                MeasuringUnit = measuringUnit,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                Quantity = quantity
            };

            this.data.Ingredients.Add(ingredient);
            this.data.Commit();
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return this.data.Ingredients.GetAll();
        }
    }
}