using Bytes2you.Validation;
using HomeMadeFood.Data.Data;
using HomeMadeFood.Models;
using HomeMadeFood.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeFood.Services.Data
{
    public class DailyMenuService : IDailyMenuService
    {
        private readonly IRecipesService recipeService;
        private readonly IHomeMadeFoodData data;

        public DailyMenuService(IHomeMadeFoodData data, IRecipesService recipeService)
        {
            Guard.WhenArgument(data, "data").IsNull().Throw();
            this.data = data;

            Guard.WhenArgument(recipeService, "recipeService").IsNull().Throw();
            this.recipeService = recipeService;
        }

        public void AddDailyMenu(DateTime date, IEnumerable<Guid> recipesIds)
        {
            Guard.WhenArgument(recipesIds, "recipesIds").IsNullOrEmpty().Throw();

            var recipesToAdd = new List<Recipe>();
            if (recipesIds.Count() > 0)
            {
                recipesToAdd = this.GetRecipesOfDailyMenu(recipesIds).ToList();
            }

            DailyMenu menu = new DailyMenu()
            {
                Date = date,
                DayOfWeek = date.DayOfWeek,
                Recipes = recipesToAdd
            };

            this.data.DailyMenus.Add(menu);
            this.data.Commit();
        }

        public IEnumerable<DailyMenu> GetAllDailyMenus()
        {
            return this.data.DailyMenus.GetAll();
        }

        private IEnumerable<Recipe> GetRecipesOfDailyMenu(IEnumerable<Guid> recipesIds)
        {
            Guard.WhenArgument(recipesIds, "recipesIds").IsNullOrEmpty().Throw();

            var recipesAsList = recipesIds.ToList();
            var allRecipes = this.data.Recipes.GetAll().ToList();
            var recipesToAdd = new List<Recipe>();
            for (int i = 0; i < allRecipes.Count; i++)
            {
                for (int j = 0; j < recipesAsList.Count; j++)
                {
                    if ((allRecipes[i].Id).Equals(recipesAsList[j]))
                    {
                        recipesToAdd.Add(allRecipes[i]);
                    }
                }                
            }

            return recipesToAdd;
        }

        public DailyMenu GetDailyMenuById(Guid id)
        {
            Guard.WhenArgument(id, "id").IsEmptyGuid().Throw();

            DailyMenu menu = this.data.DailyMenus.GetById(id);

            return menu;
        }

        public void EditDailyMenu(Guid id, DateTime date, IEnumerable<Guid> recipesIds)
        {
            Guard.WhenArgument(id, "id").IsEmptyGuid().Throw();
            Guard.WhenArgument(recipesIds, "recipesIds").IsNullOrEmpty().Throw();
            
            var dailyMenu = this.data.DailyMenus.GetById(id);

            var recipesToAdd = new List<Recipe>();
            if (recipesIds.Count() > 0)
            {
                recipesToAdd = this.GetRecipesOfDailyMenu(recipesIds).ToList();
            }

            dailyMenu.Date = date;
            dailyMenu.DayOfWeek = date.DayOfWeek;
            dailyMenu.Recipes = recipesToAdd;

            this.data.DailyMenus.Update(dailyMenu);
            this.data.Commit();
        }

        public void DeleteDailyMenu(DailyMenu menu)
        {
            Guard.WhenArgument(menu, "menu").IsNull().Throw();

            this.data.DailyMenus.Delete(menu);
            this.data.Commit();
        }
    }
}