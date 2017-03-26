using System;
using System.Collections.Generic;

using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IFoodCategoriesService
    {
        void AddFoodCategory(FoodCategory foodCategory);

        IEnumerable<FoodCategory> GetAllFoodCategories();

        FoodCategory GetFoodCategoryById(Guid id);

        FoodCategory GetFoodCategoryByName(string name);

        void EditFoodCategory(FoodCategory foodCategory);

        void DeleteFoodCategory(FoodCategory foodCategory);

        void AddIngredientCostToFoodCategory(Ingredient ingredient);

        void RemoveIngredientCostFromFoodCategory(Ingredient ingredient);

        void AddIngredientQuantityToFoodCategory(Ingredient ingredient);

        void RemoveIngredientQuantityFromFoodCategory(Ingredient ingredient);
    }
}