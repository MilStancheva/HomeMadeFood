using System;
using System.Collections.Generic;

using HomeMadeFood.Models;

namespace HomeMadeFood.Services.Data.Contracts
{
    public interface IDailyMenuService
    {
        IEnumerable<DailyMenu> GetAllDailyMenus();

        IEnumerable<DailyMenu> GetFiveDailyMenusForTheNextWeek();

        void AddDailyMenu(DateTime date, IEnumerable<Guid> recipesIds);

        void EditDailyMenu(Guid id, DateTime date, IEnumerable<Guid> recipesIds);

        void DeleteDailyMenu(DailyMenu menu);

        DailyMenu GetDailyMenuById(Guid id);

        IEnumerable<FoodCategory> GetShoppingListOfFoodCategoriesForActiveDailyMenus(IEnumerable<DailyMenu> dailyMenus);

        decimal CalculateShoppingListCostForActiveDailyMenus(IEnumerable<FoodCategory> foodCategoriesOfActiveDailyMenus);
    }
}