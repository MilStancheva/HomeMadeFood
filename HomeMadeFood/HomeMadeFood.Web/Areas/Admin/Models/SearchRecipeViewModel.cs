using System.Collections.Generic;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchRecipeViewModel
    {
        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}