using System.Collections.Generic;

namespace HomeMadeFood.Web.Areas.Admin.Models.ShoppingListViewModels
{
    public class ShoppingListViewModel
    {
        public IEnumerable<FoodCategoryViewModel> FoodCategories { get; set; }

        public decimal TotalCost { get; set; }
    }
}