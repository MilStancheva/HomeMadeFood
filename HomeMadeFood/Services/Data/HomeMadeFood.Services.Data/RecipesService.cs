using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using HomeMadeFood.Data.Data;
using HomeMadeFood.Services.Data.Contracts;
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

        public void AddRecipe(Recipe recipe, 
            IEnumerable<string> ingredientNames, 
            IEnumerable<double> ingredientQuantities, 
            IEnumerable<decimal> ingredientPrices,
            IEnumerable<Guid> foodCategories)
        {
            Guard.WhenArgument(recipe, "recipe").IsNull().Throw();

            var ingredients = new List<Ingredient>();
            var ingredientsAsList = ingredientNames.ToList();
            var quantitiesAsList = ingredientQuantities.ToList();
            var pricesAsList = ingredientPrices.ToList();
            var foodCategoriesIdsAsList = foodCategories.ToList();
            var count = ingredientsAsList.Count;

            for (int i = 0; i < count; i++)
            {
                var name = ingredientsAsList[i].ToLower();
                var quantity = quantitiesAsList[i];
                var price = pricesAsList[i];
                var foodCategoryId = foodCategoriesIdsAsList[i];
                var ingredient = new Ingredient()
                {
                    Name = name,
                    QuantityInMeasuringUnit = quantity,
                    PricePerMeasuringUnit = price,
                    FoodcategoryId = foodCategoryId
                };

                recipe.Ingredients.Add(ingredient);               
            }

            var costPerPortion = recipe.Ingredients.Select(x => x.PricePerMeasuringUnit).Sum();
            var quantityPerPortion = recipe.Ingredients.Select(x => x.QuantityInMeasuringUnit).Sum();
            recipe.CostPerPortion = costPerPortion;
            recipe.QuantityPerPortion = quantityPerPortion;

            this.data.Recipes.Add(recipe);
            this.data.Commit();
        }

        public void DeleteRecipe(Recipe recipe)
        {
            Guard.WhenArgument(recipe, "recipe").IsNull().Throw();

            this.data.Recipes.Delete(recipe);
            this.data.Commit();
        }

        public void EditRecipe(Recipe recipe)
        {
            Guard.WhenArgument(recipe, "recipe").IsNull().Throw();

            this.data.Recipes.Update(recipe);
            this.data.Commit();
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            var recipes = this.data.Recipes.GetAll();
            if (recipes == null)
            {
                return null;
            }

            return recipes.OrderBy(x => x.Id);
        }

        public Recipe GetRecipeById(Guid id)
        {
            Guard.WhenArgument(id, "id").IsEmptyGuid().Throw();

            var recipe = this.data.Recipes.GetById(id);

            if (recipe == null)
            {
                return null;
            }

            return recipe;
        }
    }
}