using HomeMadeFood.Models;
using System;
using System.Collections.Generic;

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
    }
}
