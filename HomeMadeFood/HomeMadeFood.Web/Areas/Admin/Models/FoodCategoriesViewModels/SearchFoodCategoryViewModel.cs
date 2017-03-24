using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchFoodCategoryViewModel
    {
        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        [Display(Name ="Food Categories")]
        public IEnumerable<FoodCategoryViewModel> FoodCategories { get; set; }
    }
}