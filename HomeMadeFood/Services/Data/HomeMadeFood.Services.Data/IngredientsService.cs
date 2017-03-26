using System;
using System.Collections.Generic;

using Bytes2you.Validation;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;

namespace HomeMadeFood.Services.Data
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IHomeMadeFoodData data;
        private readonly IFoodCategoriesService foodCategoriesService;

        public IngredientsService(IHomeMadeFoodData data, IFoodCategoriesService foodCategoriesService)
        {
            Guard.WhenArgument(data, "data").IsNull().Throw();
            this.data = data;

            Guard.WhenArgument(foodCategoriesService, "foodCategoriesService").IsNull().Throw();
            this.foodCategoriesService = foodCategoriesService;
        }

        public void AddIngredient(string name, Guid foodCategoryId, decimal pricePerMeasuringUnit, double quantityPerMeasuringUnit, Guid recipeId)
        {
            Guard.WhenArgument(name, "name").IsNull().Throw();
            Guard.WhenArgument(foodCategoryId, "foodCategoryId").IsEmptyGuid().Throw();
            Guard.WhenArgument(pricePerMeasuringUnit, "pricePerMeasuringUnit").IsLessThan(0).Throw();
            Guard.WhenArgument(quantityPerMeasuringUnit, "quantityPerMeasuringUnit").IsLessThan(0).Throw();
            Guard.WhenArgument(recipeId, "recipeId").IsEmptyGuid().Throw();

            Ingredient ingredient = new Ingredient()
            {
                Name = name,
                FoodCategoryId = foodCategoryId,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                QuantityInMeasuringUnit = quantityPerMeasuringUnit,
                RecipeId = recipeId
            };

            this.data.Ingredients.Add(ingredient);
            this.foodCategoriesService.AddIngredientCostToFoodCategory(ingredient);
            this.foodCategoriesService.AddIngredientQuantityToFoodCategory(ingredient);

            this.data.SaveChanges();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Guard.WhenArgument(ingredient, "ingredient").IsNull().Throw();

            this.data.Ingredients.Add(ingredient);
            this.foodCategoriesService.AddIngredientCostToFoodCategory(ingredient);
            this.foodCategoriesService.AddIngredientQuantityToFoodCategory(ingredient);

            this.data.SaveChanges();
        }

        public Ingredient CreateIngredient(string name, Guid foodCategoryId, decimal pricePerMeasuringUnit, double quantityPerMeasuringUnit)
        {
            Ingredient ingredient = new Ingredient()
            {
                Name = name,
                FoodCategoryId = foodCategoryId,
                PricePerMeasuringUnit = pricePerMeasuringUnit,
                QuantityInMeasuringUnit = quantityPerMeasuringUnit
            };

            this.foodCategoriesService.AddIngredientCostToFoodCategory(ingredient);
            this.foodCategoriesService.AddIngredientQuantityToFoodCategory(ingredient);

            return ingredient;
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            var ingredients = this.data.Ingredients.All;

            if (ingredients == null)
            {
                return null;
            }

            return ingredients;
        }

        public Ingredient GetIngredientById(Guid id)
        {
            Guard.WhenArgument(id, "id").IsEmptyGuid().Throw();

            var ingretient = this.data.Ingredients.GetById(id);

            if (ingretient == null)
            {
                return null;
            }

            return ingretient;
        }

        public void EditIngredient(Ingredient ingredient)
        {
            Guard.WhenArgument(ingredient, "ingredient").IsNull().Throw();

            this.data.Ingredients.Update(ingredient);
            this.data.SaveChanges();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            Guard.WhenArgument(ingredient, "ingredient").IsNull().Throw();

            this.foodCategoriesService.RemoveIngredientCostFromFoodCategory(ingredient);
            this.foodCategoriesService.RemoveIngredientQuantityFromFoodCategory(ingredient);

            this.data.Ingredients.Delete(ingredient);
            this.data.SaveChanges();
        }

        public IEnumerable<Ingredient> GetAllIngredientsIncludingRecipes()
        {
            return this.data.Ingredients.GetAllIncluding(x => x.Recipe);
        }
    }
}