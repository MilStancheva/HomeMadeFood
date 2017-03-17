using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeMadeFood.Web.Areas.Admin.Models
{
    public class SearchRecipeViewModel
    {
        [Display(Name = "Recipe Name")]
        public string Name { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}