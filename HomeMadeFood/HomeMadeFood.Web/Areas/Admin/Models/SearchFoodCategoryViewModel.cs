using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchFoodCategoryViewModel
    {
        [Display(Name = "Food Category Name")]
        public string Name { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        [Display(Name ="Food Categories")]
        public IEnumerable<FoodCategoryViewModel> FoodCategories { get; set; }
    }
}