using HomeMadeFood.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeMadeFood.Models;
using HomeMadeFood.Data.Data;
using Bytes2you.Validation;

namespace HomeMadeFood.Services.Data
{
    public class FoodCategoriesService : IFoodCategoriesService
    {
        private readonly IHomeMadeFoodData data;

        public FoodCategoriesService(IHomeMadeFoodData data)
        {
            Guard.WhenArgument(data, "data").IsNull().Throw();
            this.data = data;
        }

        public void AddFoodCategory(FoodCategory foodCategory)
        {
            Guard.WhenArgument(foodCategory, "foodCategory").IsNull().Throw();

            this.data.FoodCategories.Add(foodCategory);
            this.data.Commit();
        }

        public void DeleteFoodCategory(FoodCategory foodCategory)
        {
            Guard.WhenArgument(foodCategory, "foodCategory").IsNull().Throw();

            this.data.FoodCategories.Delete(foodCategory);
            this.data.Commit();
        }

        public void EditFoodCategory(FoodCategory foodCategory)
        {
            Guard.WhenArgument(foodCategory, "foodCategory").IsNull().Throw();

            this.data.FoodCategories.Update(foodCategory);
            this.data.Commit();
        }

        public IEnumerable<FoodCategory> GetAllFoodCategories()
        {
            var foodCategories = this.data.FoodCategories.GetAll();

            if (foodCategories == null)
            {
                return null;
            }

            return foodCategories.OrderBy(x => x.Id);
        }

        public FoodCategory GetFoodCategoryById(Guid id)
        {
            Guard.WhenArgument(id, "id").IsEmptyGuid().Throw();

            var foodCategory = this.data.FoodCategories.GetById(id);

            if (foodCategory == null)
            {
                return null;
            }

            return foodCategory;
        }

        public FoodCategory GetFoodCategoryByName(string name)
        {
            Guard.WhenArgument(name, "name").IsNullOrEmpty().Throw();

            var foodCategory = this.data.FoodCategories.GetAll().FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            if (foodCategory == null)
            {
                return null;
            }

            return foodCategory;
        }
    }
}
